using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBehavior : MonoBehaviour, IUpdatable
{
    float speed;
    float maxRange;
    float minRange;
    Vector3 direction;
    Transform child;
    Vector3 currentInputMovement => new Vector3(-MovementManager.movement.x, 0, -MovementManager.movement.y);

    float elapsedDistance;

    public static event Action<BoomerangBehavior> onDeactivate;

    public void Initialize(float speed, float maxRange, float minRange, Vector3 target)
    {
        this.speed = speed;
        this.maxRange = maxRange;
        this.minRange = Mathf.Pow(minRange, 2);
        direction = target.normalized;
        child = transform.GetChild(0);

        elapsedDistance = 0;
    }

    public void OnUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        float distanceToAdd = speed * Time.deltaTime;

        Vector3 dir = elapsedDistance < maxRange ? direction : -transform.position.normalized;
        transform.Translate(((speed * dir) + currentInputMovement) * Time.deltaTime);
        elapsedDistance += distanceToAdd;
        if (elapsedDistance < maxRange) return;
        if (transform.position.sqrMagnitude > minRange) return;
        gameObject.SetActive(false);
        onDeactivate?.Invoke(this);
    }

    private void Rotate()
    {
        child.transform.Rotate(360 * Time.deltaTime * Vector3.forward);
    }
}
