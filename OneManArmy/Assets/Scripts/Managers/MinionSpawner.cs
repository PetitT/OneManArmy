using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : IUpdatable
{
    GameObject minion => DataManager.runtimeData.minion;
    Vector2 movement => MovementManager.movement;
    float minSpawnDistance => DataManager.runtimeData.minSpawnDistance;
    float maxSpawnDistance => DataManager.runtimeData.maxSpawnDistance;
    float YOffset => DataManager.runtimeData.YOffset;
    float spawnDelay => DataManager.runtimeData.spawnRate;
    float remainingSpawnDelay;

    public void OnUpdate()
    {
        Timer.LoopedCountDown(ref remainingSpawnDelay, spawnDelay, SpawnMinion);
    }

    private void SpawnMinion()
    {
        GameObject newEnemy = Pool.Instance.GetItemFromPool(minion, GetRandomPoints(), Quaternion.identity);
        EnemyManager.AddEnemy(newEnemy);
    }

    private Vector3 GetRandomPoints()
    {
        float XValue = movement.x;
        float YValue = movement.y;

        int currentY = 1;
        int currentX = 1;

        float minX = 0;
        float minY = 0;
        float maxX = 0;
        float maxY = 0;

        if (YValue == 0 && XValue == 0)
        {
            bool random = HelperFunctions.GetRandomBool();
            currentY = UnityEngine.Random.Range(0, 2) * 2 - 1;
            currentX = UnityEngine.Random.Range(0, 2) * 2 - 1;

            minX = random ? 0 : minSpawnDistance;
            maxX = random ? minSpawnDistance : maxSpawnDistance;

            minY = !random ? 0 : minSpawnDistance;
            maxY = !random ? minSpawnDistance : maxSpawnDistance;
        }
        else if (YValue == 0)
        {
            currentY = UnityEngine.Random.Range(0, 2) * 2 - 1;
            currentX = XValue > 0 ? 1 : -1;
            minY = 0;
            maxY = minSpawnDistance;
            minX = minSpawnDistance;
            maxX = maxSpawnDistance;
        }
        else if (XValue == 0)
        {
            currentX = UnityEngine.Random.Range(0, 2) * 2 - 1;
            currentY = YValue > 0 ? 1 : -1;
            minX = 0;
            maxX = minSpawnDistance;
            minY = minSpawnDistance;
            maxY = maxSpawnDistance;
        }
        else
        {
            currentX = XValue > 0 ? 1 : -1;
            currentY = YValue > 0 ? 1 : -1;

            bool random = HelperFunctions.GetRandomBool();

            minX = random ? 0 : minSpawnDistance;
            maxX = random ? minSpawnDistance : maxSpawnDistance;

            minY = !random ? 0 : minSpawnDistance;
            maxY = !random ? minSpawnDistance : maxSpawnDistance;
        }

        float X = UnityEngine.Random.Range(minX, maxX) * currentX;
        float Y = UnityEngine.Random.Range(minY, maxY) * currentY;

        return new Vector3(X, YOffset, Y);
    }
}
