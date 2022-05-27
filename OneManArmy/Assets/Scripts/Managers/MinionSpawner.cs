using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : Spawner
{
    protected override GameObject objectToSpawn => DataManager.runtimeData.minion;
    protected override float spawnDelay => DataManager.runtimeData.minionSpawnRate;

    protected override Vector3 GetRandomPosition()
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

        return new Vector3(X, 0, Y);
    }
}
