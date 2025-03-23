using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cannot attach non Monobehaviour scripts (in this case,KitchenObjectSO) to Inspector so, we need brand new script
public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjParent; //to know where the obj is

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetKitchenObjParent(IKitchenObjectParent kitchenObjParent)
    {
        if (this.kitchenObjParent != null) //old parent
        {
            this.kitchenObjParent.ClearKitchenObject();
        }

        //new parent
        this.kitchenObjParent = kitchenObjParent;

        if (kitchenObjParent.HasKitchenObject())
        {
            Debug.LogError("IKitchenObjParent Already has a kitchen object");
        }

        kitchenObjParent.SetKitchenObject(this);//change current kitchenObj to new one

        transform.parent = kitchenObjParent.GetKitchenObjectFollowTransform(); // the parent of the kitchen obj changed
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjParent()
    {
        return kitchenObjParent;
    }

    public void DestorySelf()
    {
        kitchenObjParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObj(KitchenObjectSO kitchenObjectSO,IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjParent(kitchenObjectParent);

        return kitchenObject;
    }
}
