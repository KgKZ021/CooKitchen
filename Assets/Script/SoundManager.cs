using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManagerr_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        PlayerController.Instance.OnPickedSomething += Instance_OnPickedSomething;
        BaseCounter.OnAnyObjPlacedHere += BaseCounter_OnAnyObjPlacedHere;
        TrashCounter.OnAnyObjTrashed += TrashCounter_OnAnyObjTrashed;
    }

    private void TrashCounter_OnAnyObjTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = (TrashCounter)sender;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = (BaseCounter)sender;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Instance_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickUp,PlayerController.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = (CuttingCounter)sender; //which cutting counter
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter=DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail,deliveryCounter.transform.position);
      
    }

    private void DeliveryManagerr_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
       
    }

    private void PlaySound(AudioClip audioClip,Vector3 position,float volume=1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);

    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0,audioClipArray.Length)], position, volume);

    }

    public void PlayFootStepSounds(Vector3 position,float volume) //or we can just normally take with serailize field in PlayerSound.cs for audioClipArray
    {
        PlaySound(audioClipRefsSO.footStep, position, volume);
    }
}
