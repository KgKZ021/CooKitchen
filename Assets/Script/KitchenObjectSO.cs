using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scriptable Objects (can store objects as scripts in assets)
[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject // not monobehavior
{
    public Transform prefab;
    public Sprite sprite;
    public string objName;

}
