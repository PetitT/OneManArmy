using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CheatManager))]

public class CheatManagerEditor : Editor
{
    CheatManager cheatManager;
    public override void OnInspectorGUI()
    {
        cheatManager = target as CheatManager;
        base.OnInspectorGUI();
        if(GUILayout.Button("Add attack"))
        {
            cheatManager.AddAttack();
        }
        if (GUILayout.Button("Disable All Enemies"))
        {
            cheatManager.ClearAllMinions();
        }

    }

    private void OnSceneGUI()
    {
        if (GUI.Button(new Rect(10, 10, 50, 50), "Haaa"))
        {

        }
        
    }
}
