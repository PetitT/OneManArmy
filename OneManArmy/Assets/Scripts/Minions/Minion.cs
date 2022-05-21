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

        updatables.Add(movement);
    }

    public void Initialize()
    {
        health.FullyRegenerate();
    }

    private void Health_onDeath()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        updatables.ForEach(t => t.OnUpdate());
    }

    public void TakeDamage(float damage)
    {
        health.TakeDamage(damage);
    }

}
