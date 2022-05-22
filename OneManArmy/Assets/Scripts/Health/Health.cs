using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    float maxHealth;
    float currentHealth;

    public event Action onDeath;
    public event Action<float, float> onHealthChanged;

    public Health(float maxHealth)
    {
        this.maxHealth = maxHealth;
        FullyRegenerate();
    }

    public void ModifyMaxHealth(float difference)
    {
        if(difference > 0)
        {
            maxHealth += difference;
            Heal(difference);
        }
        else
        {
            maxHealth -= difference;
            TakeDamage(0);
        }
    }

    public void FullyRegenerate()
    {
        Heal(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        onHealthChanged?.Invoke(currentHealth, maxHealth);
        if(currentHealth == 0)
        {
            onDeath?.Invoke();
        }
    }

    public void Heal(float heal)
    {
        currentHealth += heal;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}
