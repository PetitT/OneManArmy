using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBarDisplay
{
    Slider xpBar;
    TMP_Text levelText;

    public ExperienceBarDisplay(Slider xpBar, TMP_Text levelText)
    {
        this.xpBar = xpBar;
        this.levelText = levelText;

        OnXpChangedEvent.RegisterListener(OnXPChanged);
        OnLevelUpEvent.RegisterListener(OnLevelUp);

        DisplayXP(0, 1);
        DisplayLevel(0);
    }

    private void OnXPChanged(OnXpChangedEvent info)
    {
        DisplayXP(info.currentXP, info.xpToLevelUp);
    }

    private void DisplayXP(float current, float max)
    {
        xpBar.value = current / max;
    }
    private void OnLevelUp(OnLevelUpEvent info)
    {
        DisplayLevel(info.newlevel);
    }

    private void DisplayLevel(int currentLevel)
    {
        levelText.text = (currentLevel + 1).ToString();
    }
}
