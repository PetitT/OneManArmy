using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BombBehavior : MonoBehaviour, IUpdatable
{
    Vector3 currentMovement => new Vector3(-MovementManager.movement.x, 0, -MovementManager.movement.y);
    Vector3 direction;

    float damage;
    float distance;
    float speed;
    float maxHeight;
    float distanceToReach;
    float remainingDistance;
    float minDistanceToTarget;

    bool hasExploded = false;

    public static event Action<BombBehavior> onDeactivate;

    public void Initialize(Vector3 targetPosition, float damage, float distance, float speed, float minDistanceToTarget, float maxHeight)
    {
        distanceToReach = targetPosition.magnitude;
        direction = targetPosition.normalized;
        this.speed = speed;
        this.minDistanceToTarget = minDistanceToTarget;
        this.maxHeight = maxHeight;
        this.damage = damage;
        this.distance = distance;
        remainingDistance = distanceToReach;
        hasExploded = false;
        transform.localScale = Vector3.one;
    }

    public void OnUpdate()
    {
        if (hasExploded)
        {
            StaySationary();
        }
        else
        {
            Move();
            MoveOnY();
        }
    }

    private void StaySationary()
    {
        transform.Translate(Time.deltaTime * currentMovement);
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
        transform.Translate(Time.deltaTime * ((speed * direction) + currentMovement));

        remainingDistance -= distanceToMove;
        if (remainingDistance < minDistanceToTarget)
        {
            hasExploded = true;
            Explode();
            AnimateExlosion();
        }
    }

    private void AnimateExlosion()
    {
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOScale(distance, 0.1f));
        s.Append(transform.DOScale(0, 0.2f));
        s.OnComplete(() =>
        {
            gameObject.SetActive(false);
            onDeactivate?.Invoke(this);
        });
        s.Play();
    }

    public void Explode()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, distance, DataManager.runtimeData.ennemyLayer);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}
