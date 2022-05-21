using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : IUpdatable
{
    public static Vector2 movement { get; private set; }

    Vector2 currentMovementInput => InputManager.currentMoveInput;
    float maxSpeed => DataManager.runtimeData.speed;
    float acceleration => DataManager.runtimeData.acceleration;

    float targetSpeed;
    float currentSpeed;
    Vector2 lastMovement;


    public void OnUpdate()
    {
        CheckIfMoving();
        AdaptSpeed();
    }

    private void CheckIfMoving()
    {
        if(currentMovementInput != Vector2.zero)
        {
            lastMovement = currentMovementInput;
            targetSpeed = maxSpeed;
        }
        else
        {
            targetSpeed = 0;
        }
    }

    private void AdaptSpeed()
    {
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        movement = lastMovement * currentSpeed;
    }

    public void Stop()
    {
        movement = Vector2.zero;
    }
}
