using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter 
{
    public event EventHandler OnPlayerGrabObj;


    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override void Interact(PlayerController player)
    {
        if (!player.HasKitchenObject())
        {
            //player isn ot carrying
            KitchenObject.SpawnKitchenObj(kitchenObjectSO,player);
            
            //kitchenObjTransform.GetComponent<KitchenObject>().SetKitchenObjParent(this); 

            OnPlayerGrabObj?.Invoke(this,EventArgs.Empty);

        }
        //not spawn on counter anymore, directly pass it to player
        //else
        //{
        //    kitchenObject.SetKitchenObjParent(player);

        //}

    }
}
