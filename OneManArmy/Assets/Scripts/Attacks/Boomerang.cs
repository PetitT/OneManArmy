using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boomerang", menuName = ("Attacks/Boomerang"))]
public class Boomerang : Attack
{
    [SerializeField] GameObject boomerang;
    [SerializeField] List<BoomerangData> data;

    BoomerangData currentData;


    public override string GetCurrentLevelUpInfo()
    {
        throw new System.NotImplementedException();
    }

    public override void OnInitialize()
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnLevelUp()
    {
        throw new System.NotImplementedException();
    }
}

[Serializable]
public class BoomerangData 
{
    public string Info;
    public float Cooldown;
    public float Damage;
    public float Speed;
    public float Range;
}
