using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{

    public static event EventHandler OnAnyObjTrashed;//Here static even though it has only one. Cuz Just to be able to handle if it has multiple
    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestorySelf();

            OnAnyObjTrashed?.Invoke(this,EventArgs.Empty);
        }
    }
}
