using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : IUpdatable
{
    public static float CurrentTime { get; private set; }

    public void OnUpdate()
    {
        CurrentTime += Time.deltaTime;
    }
}
