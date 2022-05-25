using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinionManager : IUpdatable
{
    public static List<Minion> minions = new List<Minion>();

    public MinionManager()
    {
        minions.Clear();
    }

    public static void AddMinion(Minion newEnemy)
    {
        if (!minions.Contains(newEnemy))
        {
            minions.Add(newEnemy);
        }
        else
        {
            Debug.LogWarning("Trying to add an already existing minion");
        }
    }

    public static void RemoveMinion(Minion enemy)
    {
        if (minions.Contains(enemy))
        {
            minions.Remove(enemy);
        }
        else
        {
            Debug.LogWarning("Trying to remove an enemy that is not in the list");
        }
    }

    public void OnUpdate()
    {
        foreach (var minion in minions.ToList())
        {
            minion.OnUpdate();
        }
    }

    //MIGHT NOT BE CHEAP LOL
    public static Minion GetClosestMinion()
    {
        return minions.OrderBy(t => t.transform.position.magnitude).FirstOrDefault();
    }
}
