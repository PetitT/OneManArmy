using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] Canvas gameCanvas;
    [SerializeField] Canvas levelUpCanvas;
    [SerializeField] TMP_Text levelText;
    [SerializeField] List<ButtonInfo> buttonInfos;

    private List<Attack> currentSelectedAttacks;

    private void Start()
    {
        OnLevelUpEvent.RegisterListener(DisplayLevel);
        buttonInfos[0].button.onClick.AddListener(ButtonOne);
        buttonInfos[1].button.onClick.AddListener(ButtonTwo);
        buttonInfos[2].button.onClick.AddListener(ButtonThree);
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

    public void DisplayGameCanvas()
    {
        gameCanvas.enabled = true;
        levelUpCanvas.enabled = false;
    }

    public void DisplayLevelUpCanvas()
    {
        gameCanvas.enabled = false;
        levelUpCanvas.enabled = true;
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
        currentSelectedAttacks[index].LevelUp();
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
