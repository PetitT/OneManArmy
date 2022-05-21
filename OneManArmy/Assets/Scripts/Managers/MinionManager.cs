using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinionManager
{
    public static List<Minion> minions = new List<Minion>();

    public static void AddMinion(Minion newEnemy)
    {
        minions.Add(newEnemy);
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

    //MIGHT NOT BE CHEAP LOL
    public static Minion GetClosestMinion()
    {
        return minions.OrderBy(t => t.transform.position.magnitude).FirstOrDefault();
    }
}
