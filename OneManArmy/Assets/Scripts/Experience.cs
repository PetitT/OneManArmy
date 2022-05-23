using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience
{
    float currentExperience;
    int currentLevel;

    List<float> xpTable => DataManager.runtimeData.xpTable;
    float experienceToLevelUp => xpTable[currentLevel];

    public void AddExperience(float xp)
    {
        UpdateExperience(currentExperience + xp);
        if (currentExperience >= experienceToLevelUp)
        {
            LevelUp();
        }
    }

    private void UpdateExperience(float newValue)
    {
        currentExperience = newValue;
        OnXpChangedEvent xpChangedEvent = new OnXpChangedEvent() { currentXP = currentExperience, xpToLevelUp = experienceToLevelUp };
        xpChangedEvent.FireEvent();
    }

    private void LevelUp()
    {
        UpdateExperience(0);
        currentLevel++;
        OnLevelUpEvent levelUpEvent = new OnLevelUpEvent() { newlevel = currentLevel };
        levelUpEvent.FireEvent();
    }
}
