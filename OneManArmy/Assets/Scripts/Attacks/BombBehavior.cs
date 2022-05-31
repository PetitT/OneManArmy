using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour, IUpdatable
{
    Vector3 currentMovement => new Vector3(-MovementManager.movement.x, 0, -MovementManager.movement.y);
    Vector3 direction;
    float speed;
    float maxHeight;
    float distanceToReach;
    float remainingDistance;
    float minDistanceToTarget;

    public static event Action<BombBehavior> onDeactivate;

    public void Initialize(Vector3 targetPosition, float speed, float minDistanceToTarget, float maxHeight)
    {
        distanceToReach = targetPosition.magnitude;
        direction = targetPosition.normalized;
        this.speed = speed;
        this.minDistanceToTarget = minDistanceToTarget;
        this.maxHeight = maxHeight;
        remainingDistance = distanceToReach;
    }

    public void OnUpdate()
    {
        Move();
        MoveOnY();
    }

    private void MoveOnY()
    {
        float current = remainingDistance / distanceToReach * MathF.PI;
        float currentY = MathF.Sin(current) * maxHeight;
        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
    }

    private void Move()
    {
        float distanceToMove = speed * Time.deltaTime;
        transform.Translate(((speed * direction) + currentMovement) * Time.deltaTime);

        remainingDistance -= distanceToMove;
        if(remainingDistance < minDistanceToTarget)
        {
            gameObject.SetActive(false);
            onDeactivate?.Invoke(this);
        }
    }
}
