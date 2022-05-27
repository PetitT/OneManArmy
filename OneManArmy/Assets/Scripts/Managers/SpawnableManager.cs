using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnableManager<T> : IUpdatable
{
    public static List<Spawnable> currentObjects { get; private set; }

    public SpawnableManager()
    {
        currentObjects = new List<Spawnable>();
    }
    
    public static void AddObject(Spawnable newObject)
    {
        if (!currentObjects.Contains(newObject))
        {
            currentObjects.Add(newObject);
        }
        else
        {
            Debug.LogWarning("Trying to add an object that is already in the list");
        }
    }

    public static void RemoveObject(Spawnable objectToRemove)
    {
        if (currentObjects.Contains(objectToRemove))
        {
            currentObjects.Remove(objectToRemove);
        }
        else
        {
            Debug.LogWarning("Trying to remove an object that is not in the list");
        }
    }

    public void OnUpdate()
    {
        foreach (var item in currentObjects.ToList())
        {
            item.OnUpdate();
        }
    }
}
