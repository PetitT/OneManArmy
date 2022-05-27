using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinionManager : SpawnableManager<MinionManager>
{
    public static Minion GetClosestMinion()
    {
        return currentObjects.OrderBy(t => t.transform.position.sqrMagnitude).FirstOrDefault() as Minion;
    }
}
