using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Attack : ScriptableObject
{
    public int currentLevel { get; protected set; }
    protected float remainingCooldown;

    public abstract void OnInitialize();
    public abstract void OnUpdate();
    protected abstract bool IsAttackAvailable();
    protected abstract void OnLevelUp();
    protected abstract void DoAttack();

    public void ResetLevel()
    {
        currentLevel = 0;
    }
    public void LevelUp()
    {
        if (currentLevel >= DataManager.runtimeData.attackMaxLevel) return;

        currentLevel++;
        OnLevelUp();
    }

    public abstract string GetCurrentLevelUpInfo();
}

public abstract class AttackChild<T> : Attack where T : AttackData
{
    [SerializeField] protected List<T> dataList;
    [SerializeField] protected T data;

    public override void OnInitialize()
    {
        remainingCooldown = data.Cooldown;
        data.CopyFieldsFrom(dataList[0]);
        currentLevel = 1;
    }

    public override string GetCurrentLevelUpInfo()
    {
        return dataList[currentLevel].Info;
    }

    public override void OnUpdate()
    {
        if (remainingCooldown > 0)
        {
            remainingCooldown -= Time.deltaTime;
        }
        else if (IsAttackAvailable())
        {
            DoAttack();
            remainingCooldown = data.Cooldown;
        }
    }
}

public class AttackData
{
    public string Info;
    public float Cooldown;
}