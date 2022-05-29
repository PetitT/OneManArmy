using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnableManager<T> : IUpdatable
{
    public static List<Spawnable> CurrentObjects { get; private set; }

    public SpawnableManager()
    {
        CurrentObjects = new List<Spawnable>();
    }
    
    public static void AddObject(Spawnable newObject)
    {
        if (!CurrentObjects.Contains(newObject))
        {
            CurrentObjects.Add(newObject);
        }
        else
        {
            Debug.LogWarning("Trying to add an object that is already in the list");
        }
    }

    public static void RemoveObject(Spawnable objectToRemove)
    {
        if (CurrentObjects.Contains(objectToRemove))
        {
            CurrentObjects.Remove(objectToRemove);
        }
        else
        {
            Debug.LogWarning("Trying to remove an object that is not in the list");
        }
    }

    public void OnUpdate()
    {
        foreach (var item in CurrentObjects.ToList())
        {
            item.OnUpdate();
        }
    }
}
