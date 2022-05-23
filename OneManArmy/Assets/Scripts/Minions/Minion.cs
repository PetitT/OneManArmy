using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour, IDamageable
{
    Health health;
    MinionMovement movement;
    DamageDealer damageDealer;

    List<IUpdatable> updatables = new List<IUpdatable>();

    private void Start()
    {
        health = new Health(DataManager.runtimeData.minionHealth);
        movement = new MinionMovement(transform);
        damageDealer = GetComponent<DamageDealer>();
        damageDealer.SetDamage(DataManager.runtimeData.minionDamage);

        health.onDeath += Health_onDeath;
        movement.onLeftArena += Movement_onLeftArena;

        updatables.Add(movement);
    }

    private void OnDestroy()
    {
        health.onDeath -= Health_onDeath;
        movement.onLeftArena -= Movement_onLeftArena;
    }

    private void Update()
    {
        updatables.ForEach(t => t.OnUpdate());
    }


    public void ReInitialize()
    {
        health.FullyRegenerate();
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
