using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : IUpdatable
{
    float speed => DataManager.runtimeData.minionSpeed;
    float maxDistanceFromCenter => DataManager.runtimeData.maxDistanceFromCenter;
    Vector2 currentMovement => MovementManager.movement;
    Transform transform;

    public event Action onLeftArena;

    public MinionMovement(Transform transform)
    {
        this.transform = transform;
    }
    public void OnUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 naturalMovement = new Vector3(-transform.position.x, 0, -transform.position.z).normalized;
        Vector3 inputMovement = new Vector3(-currentMovement.x, 0, -currentMovement.y);

        transform.Translate(((speed * naturalMovement) + inputMovement) * Time.deltaTime);

        if (transform.position.magnitude > maxDistanceFromCenter)
        {
            onLeftArena?.Invoke();
        }
    }
}
