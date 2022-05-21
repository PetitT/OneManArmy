using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
    [SerializeField] protected int level;
    [SerializeField] protected int damage;

    public abstract void OnInitialize();
    public abstract void OnUpdate();
}
