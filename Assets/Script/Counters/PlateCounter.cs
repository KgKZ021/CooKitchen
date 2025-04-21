using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    private float spawnPlateTimer;
    private float spaenPlateTimerMax = 4f;
    private int platesSpawnAmount;
    private int platesSpawnAmountMax=4;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spaenPlateTimerMax)
        {
            spawnPlateTimer = 0f;

            if (platesSpawnAmount < platesSpawnAmountMax)
            {
                platesSpawnAmount++;

                OnPlateSpawned?.Invoke(this,EventArgs.Empty);
            }
        }
    }
    public override void Interact(PlayerController player)
    {
        if (!player.HasKitchenObject())
        {
            //Player is empty
            if (platesSpawnAmount > 0)
            {
                //There is at least one plate heree
                platesSpawnAmount--;

                KitchenObject.SpawnKitchenObj(plateKitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this,EventArgs.Empty);
            }

        }
    }
}
