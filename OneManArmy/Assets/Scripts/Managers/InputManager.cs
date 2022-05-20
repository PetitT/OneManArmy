using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : IUpdatable
{
    public static Vector2 currentMoveInput;

    public void OnUpdate()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");
        currentMoveInput = new Vector2(X, Y).normalized;
    }
}
