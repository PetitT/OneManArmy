using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Boomerang", menuName = ("Attacks/Boomerang"))]
public class Boomerang : AttackChild<BoomerangData>
{
    [SerializeField] GameObject boomerang;
    [SerializeField] float minRange;
    Minion targetMinion;
    List<BoomerangBehavior> activeBoomerangs;

    public override void OnInitialize()
    {
        base.OnInitialize();
        activeBoomerangs = new List<BoomerangBehavior>();
        BoomerangBehavior.onDeactivate += BoomerangBehavior_onDeactivate;
    }

    protected override bool IsAttackAvailable()
    {
        targetMinion = MinionManager.GetClosestMinion();
        if (targetMinion != null) return true;

        return false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        foreach (var item in activeBoomerangs.ToList())
        {
            item.OnUpdate();
        }
    }

    protected override void OnLevelUp()
    {
        BoomerangData levelUpDatas = dataList[currentLevel - 1];
        currentData.Cooldown += levelUpDatas.Cooldown;
        currentData.Damage += levelUpDatas.Damage;
        currentData.Range += levelUpDatas.Range;
        currentData.Speed += levelUpDatas.Speed;
    }

    protected override void DoAttack()
    {
        BoomerangBehavior newBoomerang = Pool.Instance.GetItemFromPool(boomerang, Vector3.zero, Quaternion.identity).GetComponent<BoomerangBehavior>();
        newBoomerang.Initialize(currentData.Speed, currentData.Range, minRange, targetMinion.transform.position);
        newBoomerang.GetComponent<DamageDealer>().SetDamage(currentData.Damage);
        activeBoomerangs.Add(newBoomerang);
    }

    private void BoomerangBehavior_onDeactivate(BoomerangBehavior obj)
    {
        if (activeBoomerangs.Contains(obj))
        {
            activeBoomerangs.Remove(obj);
        }
    }
}

[Serializable]
public class BoomerangData : AttackData
{
    public float Damage;
    public float Speed;
    public float Range;
}
