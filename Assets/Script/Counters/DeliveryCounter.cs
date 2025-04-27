using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; } //There is only one Delivery Counter

    private void Awake()
    {
        Instance = this;
    }
    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObj plateKitchenObj))
            {
                //Only accepts plates

                DeliveryManager.Instance.DeliverRecipe(plateKitchenObj);

                player.GetKitchenObject().DestorySelf();
            }
            
        }
    }
}
