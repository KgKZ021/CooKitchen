using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cannot attach non Monobehaviour scripts (in this case,KitchenObjectSO) to Inspector so, we need brand new script
public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
}
