using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    private IHasProgress hasProgress;

    [SerializeField] private GameObject hasProgressGameObject; // this is because unity inspector cannot accept Interfaces;
    [SerializeField] private Image barImage;

    private void Start()
    {
        hasProgress=hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null)
        {
            Debug.LogError("Game Object " + hasProgressGameObject + "does not have a component that implements IHasProgress");
        }
        hasProgress.OnProgressChange += HasProgress_OnProgressChange;
        barImage.fillAmount = 0f;

        Hide(); //Show() and Hide() should only be called after Listening to event
    }

    private void HasProgress_OnProgressChange(object sender, IHasProgress.OnProgressChangeEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        if(e.progressNormalized == 0f || e.progressNormalized==1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
