using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    private List<Attack> attacks => AttacksSelect.availableAttacks;
    public bool displayCheatMenu;
    public void AddAttack(Attack attack)
    {
        CombatManager.LevelUpAttack(attack);
    }

    public void ClearAllMinions()
    {
        foreach (var minion in MinionManager.CurrentObjects)
        {
            minion.gameObject.SetActive(false);
        }
    }

    public void ToggleGodMode()
    {
        FindObjectOfType<Character>().ToggleInvulnerability();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 100, 0, 100, 25), "Cheat menu"))
        {
            displayCheatMenu = !displayCheatMenu;
        }

        if (!displayCheatMenu) return;

        for (int i = 0; i < attacks.Count; i++)
        {
            if (GUI.Button(new Rect(0, i * 25, 125, 25), $"{attacks[i].name} - {attacks[i].currentLevel}"))
            {
                AddAttack(attacks[i]);
            }
        }

        if(GUI.Button(new Rect(Screen.width - 100, 25,100,25), "Invulnerable"))
        {
            ToggleGodMode();
        }
    }
}
