using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boomerang", menuName = ("Attacks/Boomerang"))]
public class Boomerang : AttackChild<BoomerangData>
{
    [SerializeField] GameObject boomerang;

    protected override void DoAttack()
    {
        throw new NotImplementedException();
    }

    protected override bool IsAttackAvailable()
    {
        throw new NotImplementedException();
    }

    protected override void OnLevelUp()
    {
        throw new NotImplementedException();
    }
}

[Serializable]
public class BoomerangData : AttackData
{
    public float Damage;
    public float Speed;
    public float Range;
}
