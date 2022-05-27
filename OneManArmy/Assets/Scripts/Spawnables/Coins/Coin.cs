using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Spawnable
{
    protected override void Initialize()
    {
        movement = new CoinMovement(transform);
        updatables.Add(movement);

        movement.onLeftArena += Movement_onLeftArena;
    }

    private void OnEnable()
    {
        CoinManager.AddObject(this);
    }

    private void OnDisable()
    {
        CoinManager.RemoveObject(this);
    }

    private void OnDestroy()
    {
        movement.onLeftArena -= Movement_onLeftArena;
    }

    private void Movement_onLeftArena()
    {
        gameObject.SetActive(false);
    }
}
