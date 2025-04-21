using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObj : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if(!validKitchenObjSOList.Contains(kitchenObjectSO)) 
        { 
            //not a valid ingredients
            return false; 
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //Already have this type
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjSO = kitchenObjectSO,
            });
            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
