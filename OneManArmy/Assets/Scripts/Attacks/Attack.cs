using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : ScriptableObject, ISerializationCallbackReceiver
{
    public int currentLevel { get; protected set; }

    public abstract void OnInitialize();
    public abstract void OnUpdate();
    protected abstract void OnLevelUp();

    public void LevelUp()
    {
        if (currentLevel >= DataManager.runtimeData.attackMaxLevel) return;

        currentLevel++;
        OnLevelUp();
    }


    public void OnBeforeSerialize() { }

    public void OnAfterDeserialize()
    {
        currentLevel = 0;
    }


}
