using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager
{
    public static List<GameObject> enemies = new List<GameObject>();

    public static void AddEnemy(GameObject newEnemy)
    {
        enemies.Add(newEnemy);
    }

    public static void RemoveEnemy(GameObject enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
        else
        {
            Debug.LogWarning("Trying to remove an enemy that is not in the list");
        }
    }

    //MIGHT NOT BE CHEAP LOL
    public static GameObject GetClosestEnemy()
    {
        return enemies.OrderBy(t => t.transform.position.magnitude).FirstOrDefault();
    }
}
