using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager 
{
    private GameData defaultData;
    public static GameData runtimeData;
    
    public DataManager()
    {
        runtimeData = Resources.Load<GameData>("RuntimeData");
        defaultData = Resources.Load<GameData>("DefaultData");
        runtimeData.CopyValues(defaultData);
    }
}
