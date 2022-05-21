using LowTeeGames;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour, IDamageable
{
    public static Transform body;
    private Health health;

    private void Start()
    {
        body = transform;
        health = new Health(DataManager.runtimeData.health);
        health.onDeath += Health_onDeath;
    }

    private void Health_onDeath()
    {
        OnPlayerDeathEvent onPlayerDeath = new OnPlayerDeathEvent();
        onPlayerDeath.FireEvent();
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        health.TakeDamage(damage);
    }
}
