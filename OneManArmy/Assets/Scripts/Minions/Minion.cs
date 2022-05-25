using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour, IDamageable, IUpdatable
{
    Health health;
    MinionMovement movement;
    DamageDealer damageDealer;
    List<IUpdatable> updatables = new List<IUpdatable>();
    bool initialized = false;

    private void Initialize()
    {
        health = new Health(DataManager.runtimeData.minionHealth);
        movement = new MinionMovement(transform);
        damageDealer = GetComponent<DamageDealer>();

        damageDealer.SetDamage(DataManager.runtimeData.minionDamage);
        updatables.Add(movement);

        health.onDeath += Health_onDeath;
        movement.onLeftArena += Movement_onLeftArena;

        initialized = true;

    }

    private void OnEnable()
    {
        if (!initialized) { Initialize(); }
        health.FullyRegenerate();
        MinionManager.AddMinion(this);
    }

    private void OnDisable()
    {
        MinionManager.RemoveMinion(this);
    }

    private void OnDestroy()
    {
        health.onDeath -= Health_onDeath;
        movement.onLeftArena -= Movement_onLeftArena;
    }

    public void OnUpdate()
    {
        updatables.ForEach(t => t.OnUpdate());
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
