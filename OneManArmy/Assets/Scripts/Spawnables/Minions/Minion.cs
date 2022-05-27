using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Spawnable
{
    Health health;
    DamageDealer damageDealer;

    protected override void Initialize()
    {
        health = new Health(DataManager.runtimeData.minionHealth);
        movement = new MinionMovement(transform);
        damageDealer = GetComponent<DamageDealer>();
        updatables.Add(movement);

        damageDealer.SetDamage(DataManager.runtimeData.minionDamage);

        health.onDeath += Health_onDeath;
        movement.onLeftArena += Movement_onLeftArena;
    }

    private void OnEnable()
    {
        health.FullyRegenerate();
        MinionManager.AddObject(this);
    }

    private void OnDisable()
    {
        MinionManager.RemoveObject(this);
    }

    private void OnDestroy()
    {
        movement.onLeftArena -= Movement_onLeftArena;
        health.onDeath -= Health_onDeath;
    }

    private void Health_onDeath()
    {
        OnMinionDeathEvent onMinionDeath = new OnMinionDeathEvent();
        onMinionDeath.FireEvent();
        gameObject.SetActive(false);
    }
    private void Movement_onLeftArena()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        health.TakeDamage(damage);
    }
}