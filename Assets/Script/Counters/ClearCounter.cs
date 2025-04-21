using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    //[SerializeField] private ClearCounter secondCounter;
    //[SerializeField] private bool testing;


    //private void Update()
    //{
    //    //testing
    //    if (testing && Input.GetKeyDown(KeyCode.T))
    //    {
    //        if (kitchenObject != null)
    //        {
    //            kitchenObject.SetKitchenObjParent(secondCounter);
    //        }
    //    }
        
    //}

    public override void Interact(PlayerController player)
    {
        //Clear Counter will not spawn anything
        //just pickup and drop

        if (!HasKitchenObject())
        {
            //There is no kitchen obj
            if (player.HasKitchenObject())
            {
                //Player is carrying sth
                player.GetKitchenObject().SetKitchenObjParent(this);
            }
            else
            {
                //Player has nothing
            }

        }
        else
        {
            //There is kitchen Obj
            if (player.HasKitchenObject())
            {
                //player carrying sth
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObj plateKitchenObj))
                {
                    //Player is holding the plate
                    if (plateKitchenObj.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestorySelf();
                    }
                   
                }
                else
                {
                    //Player is not holding a plate but sth else
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObj))
                    {
                        //Counter is holding a plate
                        if (plateKitchenObj.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())){
                            player.GetKitchenObject().DestorySelf();
                        }
                    }
                }
                
            }
            else
            {
                GetKitchenObject().SetKitchenObjParent(player);
            }
        }

    }
}
