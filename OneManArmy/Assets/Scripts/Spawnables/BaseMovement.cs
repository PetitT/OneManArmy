using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMovement : IUpdatable
{
    float maxDistanceFromCenter => DataManager.runtimeData.maxDistanceFromCenter;
    protected Vector2 currentMovement => MovementManager.movement;
    protected Transform transform;

    public Action onLeftArena;

    public BaseMovement(Transform transform)
    {
        this.transform = transform;
    }


    public virtual void OnUpdate()
    {
        Move();
        CheckArenaBounds();
    }

    protected abstract void Move();

    private void CheckArenaBounds()
    {
        if (transform.position.magnitude > maxDistanceFromCenter)
        {
            onLeftArena?.Invoke();
        }
    }
}
