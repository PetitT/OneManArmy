using LowTeeGames;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour, IDamageable
{
    public static Transform body;
    [SerializeField] Slider healthBar; 
    Health health;
    HealthBarDisplay healthBarDisplay;

    List<IUpdatable> updatables = new List<IUpdatable>();

    private void Start()
    {
        body = transform;
        health = new Health(DataManager.runtimeData.health);
        healthBarDisplay = new HealthBarDisplay(healthBar, health);

        updatables.Add(healthBarDisplay);

        health.onDeath += Health_onDeath;
    }

    private void Update()
    {
        updatables.ForEach(t => t.OnUpdate());
    }

    private void OnDestroy()
    {
        health.onDeath -= Health_onDeath;
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
