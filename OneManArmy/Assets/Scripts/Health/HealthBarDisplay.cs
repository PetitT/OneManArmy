using LowTeeGames;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarDisplay : IUpdatable
{
    Slider healthSlider;
    float remainingDisplayTime;
    float displayTime => DataManager.runtimeData.healthBarDisplayTime;

    public HealthBarDisplay(Slider slider, Health health)
    {
        healthSlider = slider;
        healthSlider.gameObject.SetActive(false);
        health.onHealthChanged += Health_onHealthChanged;
    }


    private void Health_onHealthChanged(float current, float max)
    {
        float percent = current / max;
        healthSlider.value = percent;
        healthSlider.gameObject.SetActive(true);
        remainingDisplayTime = displayTime;
    }

    public void OnUpdate()
    {
        Timer.CountDown(ref remainingDisplayTime, () => healthSlider.gameObject.SetActive(false));
    }
}
