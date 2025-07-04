using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private PlayerController player;
    private float footStepTimer;
    private float footStepTimerMax=0.1f;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        footStepTimer -= Time.deltaTime;
        if(footStepTimer< 0)
        {
            footStepTimer = footStepTimerMax;

            if (player.IsWalking())
            {
                float volume = 1f;
                SoundManager.Instance.PlayFootStepSounds(player.transform.position, volume);
            }
            
        }
    }
}
