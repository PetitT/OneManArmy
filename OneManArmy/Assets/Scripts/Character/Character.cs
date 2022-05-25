using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour, IDamageable
{
    public static Transform body;
    [SerializeField] Slider healthBar; 
    [SerializeField] Slider xpbar; 
    [SerializeField] TMP_Text levelText; 

    Health health;
    HealthBarDisplay healthBarDisplay;
    Experience experience;
    ExperienceBarDisplay xpBarDisplay;

    List<IUpdatable> updatables = new List<IUpdatable>();

    private void Start()
    {
        body = transform;
        health = new Health(DataManager.runtimeData.health);
        healthBarDisplay = new HealthBarDisplay(healthBar, health);
        experience = new Experience();
        xpBarDisplay = new ExperienceBarDisplay(xpbar, levelText);

        updatables.Add(healthBarDisplay);

        health.onDeath += Health_onDeath;
        OnMinionDeathEvent.RegisterListener(OnMinionDeath);
    }

    private void Update()
    {
        updatables.ForEach(t => t.OnUpdate());
    }

    private void OnDestroy()
    {
        health.onDeath -= Health_onDeath;
        OnMinionDeathEvent.UnregisterListener(OnMinionDeath);
    }

    private void Health_onDeath()
    {
        OnPlayerDeathEvent onPlayerDeath = new OnPlayerDeathEvent();
        onPlayerDeath.FireEvent();
        gameObject.SetActive(false);
    }

    private void OnMinionDeath(OnMinionDeathEvent info)
    {
        experience.AddExperience(DataManager.runtimeData.xpPerMinionKill);
    }

    public void TakeDamage(float damage)
    {
        health.TakeDamage(damage);
    }
}
