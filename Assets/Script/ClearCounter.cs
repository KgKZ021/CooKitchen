using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO; 
    [SerializeField] private Transform counterTopPoint;//can use both Transform or GameObject to get the gameObject
    //[SerializeField] private ClearCounter secondCounter;
    //[SerializeField] private bool testing;

    private KitchenObject kitchenObject;

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

    public void Interact(PlayerController player)
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);// to spawn above counter,create an empty game object
            kitchenObjTransform.GetComponent<KitchenObject>().SetKitchenObjParent(this);
            
        }
        else
        {
            //Give obj to player
            kitchenObject.SetKitchenObjParent(player);
           
        }

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
