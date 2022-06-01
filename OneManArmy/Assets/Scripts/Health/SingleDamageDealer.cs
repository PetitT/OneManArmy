using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDamageDealer : DamageDealer
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamageable damageable))
        {
            DealDamage(damageable);
        }
    }
}
