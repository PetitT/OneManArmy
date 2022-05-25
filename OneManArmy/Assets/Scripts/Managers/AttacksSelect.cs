using LowTeeGames;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttacksSelect
{
    public static List<Attack> availableAttacks;

    public AttacksSelect(List<Attack> attacks)
    {
        availableAttacks = attacks;
        availableAttacks.ForEach(t => t.Reset());
    }

    public static List<Attack> GetRandomAttacksSelection(int amount)
    {
        List<Attack> attacksToSelectFrom = availableAttacks.Where(t => t.currentLevel < DataManager.runtimeData.attackMaxLevel).ToList();

        List<Attack> attacks = new List<Attack>();

        for (int i = 0; i < amount; i++)
        {
            if (attacksToSelectFrom.Count > 0)
            {
                Attack newAttack = attacksToSelectFrom.GetRandom();
                attacks.Add(newAttack);
                attacksToSelectFrom.Remove(newAttack);
            }
            else
            {
                attacks.Add(null);
            }
        }

        return attacks;
    }
}
