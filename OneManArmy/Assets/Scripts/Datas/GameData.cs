using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = ("GameData"))]
public class GameData : ScriptableObject
{
    [Header("Movement")]
    public float speed;
    public float acceleration;
    public float groundTextureSpeedMultiplicator;

    [Header("Health")]
    public float health;
    public float invincibilityTime;
    public float healthBarDisplayTime;

    [Header("Spawn")]
    public GameObject minion;
    public float minSpawnDistance;
    public float maxSpawnDistance;
    public float maxDistanceFromCenter;
    public float YOffset;
    public float spawnRate;

    [Header("Minions")]
    public float minionSpeed;
    public float minionHealth;
    public float minionDamage;

    [Header("Experience")]
    public List<float> xpTable;
    public float xpPerMinionKill;

    [Header("Rules")]
    public int attackMaxLevel;

    //public void CopyValues(GameData source)
    //{
    //    FieldInfo[] fields = GetType().GetFields();
    //    for (int i = 0; i < fields.Length; i++)
    //    {
    //        GetType().GetField(fields[i].Name).SetValue(this, fields[i].GetValue(source));
    //    }
    //}
}
