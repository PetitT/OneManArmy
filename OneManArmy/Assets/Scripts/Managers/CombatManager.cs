using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : IUpdatable
{
    public static List<Attack> attacks = new List<Attack>();

    public static void AddAttack(Attack newAttack)
    {
        attacks.Add(newAttack);
        newAttack.OnInitialize();
    }

    public void OnUpdate()
    {
        attacks.ForEach(t => t.OnUpdate());
    }
}
