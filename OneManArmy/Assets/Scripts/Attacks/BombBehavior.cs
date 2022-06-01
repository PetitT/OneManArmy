using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour, IUpdatable
{
    Vector3 currentMovement => new Vector3(-MovementManager.movement.x, 0, -MovementManager.movement.y);
    Vector3 direction;
    Vector2 damageRange;
    Vector2 distanceFalloff;

    float speed;
    float maxHeight;
    float distanceToReach;
    float remainingDistance;
    float minDistanceToTarget;

    public static event Action<BombBehavior> onDeactivate;

    public void Initialize(Vector3 targetPosition, Vector2 damageRange, Vector2 distanceFalloff, float speed, float minDistanceToTarget, float maxHeight)
    {
        distanceToReach = targetPosition.magnitude;
        direction = targetPosition.normalized;
        this.speed = speed;
        this.minDistanceToTarget = minDistanceToTarget;
        this.maxHeight = maxHeight;
        this.damageRange = damageRange;
        this.distanceFalloff = distanceFalloff;
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
            Explode();
            gameObject.SetActive(false);
            onDeactivate?.Invoke(this);
        }
    }

    public void Explode()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, distanceFalloff.y);
        for (int i = 0; i < cols.Length; i++)
        {
            if(cols[i].TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damageRange.y);
            }
        }
    }
}
