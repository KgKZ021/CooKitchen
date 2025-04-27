using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjPlacedHere;//Again,static because there are many counters
    [SerializeField] private Transform counterTopPoint;//can use both Transform or GameObject to get the gameObject

    private KitchenObject kitchenObject;

    //virtual : to be able to define in child classes
    public virtual void Interact(PlayerController player)
    {
        Debug.LogError("BaseCounter.Intercat();");
    }

    public virtual void InteractAlternate(PlayerController player)
    {
        //Debug.LogError("BaseCounter.Intercat();");
    }
    //get the position of new parent counter
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    //set the old parent counter
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null)
        {
            OnAnyObjPlacedHere?.Invoke(this,EventArgs.Empty);
        }
    }

    //get the old parent counter
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
