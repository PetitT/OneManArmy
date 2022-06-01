using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousDamageDealer : DamageDealer
{
    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out IDamageable damageable))
        {
            DealDamage(damageable);
        }
    }
}
