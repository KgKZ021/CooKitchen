using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter; //the counter itself
    [SerializeField] private GameObject[] visualGameObjectArray;
    private void Start()
    {
        //Listensers should be on Start()because if it is in Awake(), it can happen before the PlayerCOntroller Awake()
        PlayerController.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, PlayerController.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter==baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObj in visualGameObjectArray)
        {
            visualGameObj.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject visualGameObj in visualGameObjectArray)
        {
            visualGameObj.SetActive(false);
        }
    }
}
