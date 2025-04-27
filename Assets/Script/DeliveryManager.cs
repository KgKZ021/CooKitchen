using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public static DeliveryManager Instance { get; private set; } //singleton

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingrecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;//4sec
    private int waitingRecipeMax=4;

    private void Awake()
    {
        Instance = this;
        waitingrecipeSOList=new List<RecipeSO>();
    }
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingrecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSoList[UnityEngine.Random.Range(0, recipeListSO.recipeSoList.Count)];
               
                waitingrecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObj plateKitchenObj)
    {
        for(int i = 0; i < waitingrecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingrecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObj.GetKitchenObjectSOList().Count)
            {//same number of ingredients
                bool plateContentMatchesRecipe=true;
                foreach (KitchenObjectSO recipekitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {//Cycling through all ingredients in the recipe

                    bool ingredientFound=false;
                    foreach (KitchenObjectSO plateKitchenObjSO in plateKitchenObj.GetKitchenObjectSOList())
                    {//Cycling through all ingredients in the List

                        if(plateKitchenObjSO==recipekitchenObjectSO)
                        {//Ingredient matches

                            ingredientFound = true;
                            break;
                        }

                    }
                    if (!ingredientFound)
                    {//This recipe ingredient was not found on the plate
                        plateContentMatchesRecipe= false;
                    }
                        
                }
                if(plateContentMatchesRecipe)
                {//Player  delivered the correct Recipe

                    waitingrecipeSOList.RemoveAt(i);
                    
                    OnRecipeCompleted?.Invoke(this,EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);

                    return;
                }
            }
        }

        //No matches found!
        //Player did not deliver a correct Recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingrecipeSOList;
    }
}
