using LowTeeGames;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinionManager : SpawnableManager<MinionManager>
{
    public static Minion GetClosestMinion()
    {
        return CurrentObjects.OrderBy(t => t.transform.position.sqrMagnitude).FirstOrDefault() as Minion;
    }

    public static Minion GetRandomMinionInRange(float maxRange)
    {
        float sqrRange = Mathf.Pow(maxRange, 2);
        return CurrentObjects.Where(t => t.transform.position.sqrMagnitude < sqrRange).ToList().GetRandom() as Minion;
    }
}
