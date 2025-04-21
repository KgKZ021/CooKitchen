using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent 
{
    public Transform GetKitchenObjectFollowTransform();

    //set the old parent counter
    public void SetKitchenObject(KitchenObject kitchenObject);

    //get the old parent counter
    public KitchenObject GetKitchenObject();
    public void ClearKitchenObject();

    public bool HasKitchenObject();
}
