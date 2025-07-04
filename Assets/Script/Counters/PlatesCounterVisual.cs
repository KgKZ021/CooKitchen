using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private PlateCounter plateCounter;

    private List<GameObject> plateVisualGameObjectList; // to know how many plates to spawn visually

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }
    private void Start()
    {
        plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
    }

    private void PlateCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count-1]; //get last and remove
        plateVisualGameObjectList.Remove(plateGameObject);

        Destroy(plateGameObject);
    }

    private void PlateCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform=Instantiate(plateVisualPrefab,counterTopPoint);

        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition=new Vector3(0,plateOffsetY*plateVisualGameObjectList.Count,0);

        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
