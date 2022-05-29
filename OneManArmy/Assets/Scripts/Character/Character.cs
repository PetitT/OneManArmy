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
    CoinMagnet coinMagnet;

    bool isVulnerable = true;

    List<IUpdatable> updatables = new List<IUpdatable>();

    private void Start()
    {
        body = transform;
        health = new Health(DataManager.runtimeData.health);
        healthBarDisplay = new HealthBarDisplay(healthBar, health);
        experience = new Experience();
        xpBarDisplay = new ExperienceBarDisplay(xpbar, levelText);
        coinMagnet = new CoinMagnet();

        updatables.Add(coinMagnet);
        updatables.Add(healthBarDisplay);

        health.onDeath += Health_onDeath;
        OnMinionDeathEvent.RegisterListener(OnMinionDeath);
        OnCoinCollectedEvent.RegisterListener(OnCoinCollected);
    }

    private void Update()
    {
        updatables.ForEach(t => t.OnUpdate());
    }

    private void OnDestroy()
    {
        health.onDeath -= Health_onDeath;
        OnMinionDeathEvent.UnregisterListener(OnMinionDeath);
        OnCoinCollectedEvent.UnregisterListener(OnCoinCollected);
    }

    private void Health_onDeath()
    {
        OnPlayerDeathEvent onPlayerDeath = new OnPlayerDeathEvent();
        onPlayerDeath.FireEvent();
        gameObject.SetActive(false);
    }

    private void OnMinionDeath(OnMinionDeathEvent info)
    {
        experience.AddExperience(DataManager.runtimeData.experiencePerMinionKill);
    }

    private void OnCoinCollected(OnCoinCollectedEvent info)
    {
        experience.AddExperience(DataManager.runtimeData.experiencePerCoin);
    }

    public void TakeDamage(float damage)
    {
        if (!isVulnerable) return;
        health.TakeDamage(damage);
    }

    public void ToggleInvulnerability()
    {
        isVulnerable = !isVulnerable;
    }
}
