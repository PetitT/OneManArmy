using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : IUpdatable
{
    public static List<Attack> attacks { get; private set; }

    public CombatManager()
    {
        attacks = new List<Attack>();
        attacks.Clear();
    }

    public static void LevelUpAttack(Attack newAttack)
    {
        if (!attacks.Contains(newAttack))
        {
            attacks.Add(newAttack);
            newAttack.OnInitialize();
        }
        else
        {
            newAttack.LevelUp();
        }
    }

    public void OnUpdate()
    {
        attacks.ForEach(t => t.OnUpdate());
    }
}
