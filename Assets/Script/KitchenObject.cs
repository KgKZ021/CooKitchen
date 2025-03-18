using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cannot attach non Monobehaviour scripts (in this case,KitchenObjectSO) to Inspector so, we need brand new script
public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private ClearCounter clearCounter; //to know where the obj is

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if (this.clearCounter != null) //old parent
        {
            this.clearCounter.ClearKitchenObject();
        }

        //new parent
        this.clearCounter = clearCounter;

        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter Already has a kitchen object");
        }

        clearCounter.SetKitchenObject(this);//change current counter to new one

        transform.parent = clearCounter.GetKitchenObjectFollowTransform(); // the parent of the kitchen obj changed
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
