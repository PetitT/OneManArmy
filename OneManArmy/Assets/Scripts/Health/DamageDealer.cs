using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageDealer : MonoBehaviour
{
    private float damage;
    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    protected void DealDamage(IDamageable damageable)
    {
        damageable.TakeDamage(damage);
    }
}
