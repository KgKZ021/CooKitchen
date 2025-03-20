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
                
            }
            else
            {
                GetKitchenObject().SetKitchenObjParent(player);
            }
        }

    }
}
