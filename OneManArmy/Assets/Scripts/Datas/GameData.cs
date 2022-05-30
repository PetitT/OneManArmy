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
    public float minSpawnDistance;
    public float maxSpawnDistance;
    public float maxDistanceFromCenter;

    [Header("Minions")]
    public GameObject minion;
    public float minionSpeed;
    public float minionHealth;
    public float minionDamage;
    public float minionSpawnPerSec;

    [Header("Coins")]
    public GameObject coin;
    public float coinSpawnPerSec;
    public float coinGrabDistance;
    public float experiencePerCoin;

    [Header("Experience")]
    public List<float> xpTable;
    public float experiencePerMinionKill;

    [Header("Rules")]
    public int attackMaxLevel;
}
