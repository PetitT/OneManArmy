using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameData))]
public class GameDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(10);

        if (GUILayout.Button("Copy Values To Default"))
        {
            GameData runtimeData = Resources.Load<GameData>("RuntimeData");
            GameData defaultData = Resources.Load<GameData>("DefaultData");
            defaultData.CopyFieldsFrom(runtimeData);
        }
    }
}
