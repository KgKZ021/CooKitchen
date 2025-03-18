using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO; 
    [SerializeField] private Transform counterTopPoint;//can use both Transform or GameObject to get the gameObject
    public void Interact()
    {
        Debug.Log("Interact");
        Transform kitchenObjTransform=Instantiate(kitchenObjectSO.prefab,counterTopPoint);// to spawn above counter,create an empty game object
        kitchenObjTransform.localPosition=Vector3.zero;

        Debug.Log(kitchenObjTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objName);

    }
}
