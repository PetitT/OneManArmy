using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Bomb", menuName = "Attacks/Bomb")]
public class Bomb : AttackChild<BombData>
{
    [SerializeField] GameObject bomb;
    [SerializeField] float minDistanceToTarget;
    [SerializeField] float maxHeight;
    [SerializeField] float maxAttackRange;
    Minion target;
    List<BombBehavior> currentBombs;

    public override void OnInitialize()
    {
        base.OnInitialize();
        currentBombs = new List<BombBehavior>();
        BombBehavior.onDeactivate += BombBehavior_onDeactivate;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        foreach (var bomb in currentBombs.ToList())
        {
            bomb.OnUpdate();
        }
    }

    private void BombBehavior_onDeactivate(BombBehavior obj)
    {
        if (currentBombs.Contains(obj))
        {
            currentBombs.Remove(obj);
        }
    }

    protected override void OnLevelUp()
    {
        BombData newData = dataList[currentLevel - 1];
        currentData.Cooldown += newData.Cooldown;
        currentData.Damage += newData.Damage;
        currentData.ExplosionRange += newData.ExplosionRange;
    }

    protected override bool IsAttackAvailable()
    {
        target = MinionManager.GetRandomMinionInRange(maxAttackRange);
        return target != null;
    }


    protected override void DoAttack()
    {
        BombBehavior newBomb = Pool.Instance.GetItemFromPool(bomb, Vector3.zero, Quaternion.identity).GetComponent<BombBehavior>();
        newBomb.Initialize(target.transform.position, currentData.Damage, currentData.ExplosionRange, currentData.Speed, minDistanceToTarget, maxHeight);
        currentBombs.Add(newBomb);
    }


}

[Serializable]
public class BombData : AttackData
{
    public float Damage;
    public float ExplosionRange;
    public float Speed;
}