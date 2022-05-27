using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Spawnable : MonoBehaviour, IUpdatable 
{
    protected BaseMovement movement;
    protected List<IUpdatable> updatables = new List<IUpdatable>();

    private void Awake()
    {
        Initialize();
    }

    protected abstract void Initialize();

    public virtual void OnUpdate()
    {
        updatables.ForEach(t => t.OnUpdate());
    }
}
