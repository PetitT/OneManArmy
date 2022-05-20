using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
    [SerializeField] int level;

    public abstract void OnInitialize();
    public abstract void OnUpdate();
}
