using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] Canvas gameCanvas;
    [SerializeField] Canvas levelUpCanvas;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] List<ButtonInfo> buttonInfos;

    private List<Attack> currentSelectedAttacks;

    private void Start()
    {
        OnLevelUpEvent.RegisterListener(DisplayLevel);
        buttonInfos[0].button.onClick.AddListener(ButtonOne);
        buttonInfos[1].button.onClick.AddListener(ButtonTwo);
        buttonInfos[2].button.onClick.AddListener(ButtonThree);

        InvokeRepeating("DisplayCurrentTime", 0, 1);
    }

    private void OnDestroy()
    {
        OnLevelUpEvent.UnregisterListener(DisplayLevel);
        buttonInfos[0].button.onClick.RemoveListener(ButtonOne);
        buttonInfos[1].button.onClick.RemoveListener(ButtonTwo);
        buttonInfos[2].button.onClick.RemoveListener(ButtonThree);
    }

    private void DisplayLevel(OnLevelUpEvent info)
    {
        levelText.text = $"Level {info.newlevel}";
    }

    private void DisplayCurrentTime()
    {
        int seconds = (int)TimeManager.CurrentTime % 60;
        int minutes = ((int)TimeManager.CurrentTime - seconds)/60;
        StringBuilder sb = new StringBuilder();
        sb.Append(minutes);
        sb.Append(":");
        if(seconds < 10) { sb.Append("0"); }
        sb.Append(seconds);
        timeText.text = sb.ToString();
    }

    public void DisplayGameCanvas()
    {
        gameCanvas.enabled = true;
        levelUpCanvas.enabled = false;
        eventSystem.enabled = false;
    }

    public void DisplayLevelUpCanvas()
    {
        gameCanvas.enabled = false;
        levelUpCanvas.enabled = true;
        eventSystem.enabled = true;
        currentSelectedAttacks = AttacksSelect.GetRandomAttacksSelection(3);
        for (int i = 0; i < currentSelectedAttacks.Count; i++)
        {
            ButtonInfo buttonInfo = buttonInfos[i];
            if (currentSelectedAttacks[i] != null)
            {
                buttonInfo.title.text = $"{currentSelectedAttacks[i].name} - {currentSelectedAttacks[i].currentLevel}";
                buttonInfo.description.text = currentSelectedAttacks[i].GetCurrentLevelUpInfo();
                buttonInfo.button.enabled = true;
            }
            else
            {
                buttonInfo.title.text = "No available upgrade";
                buttonInfo.description.text = "";
                buttonInfo.button.enabled = false;
            }
        }
    }

    private void ButtonOne()
    {
        PressButton(0);
    }
    private void ButtonTwo()
    {
        PressButton(1);
    }
    private void ButtonThree()
    {
        PressButton(2);
    }

    private void PressButton(int index)
    {
        CombatManager.LevelUpAttack(currentSelectedAttacks[index]);
        OnSpellLevelUpEvent onSpellLevelUp = new OnSpellLevelUpEvent();
        onSpellLevelUp.FireEvent();
    }

    [Serializable]
    private class ButtonInfo
    {
        public Button button;
        public TMP_Text title;
        public TMP_Text description;
    }
}
