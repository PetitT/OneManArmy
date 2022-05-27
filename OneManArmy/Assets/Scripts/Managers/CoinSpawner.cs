using LowTeeGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : Spawner
{
    protected override GameObject objectToSpawn => DataManager.runtimeData.coin;
    protected override float spawnDelay => DataManager.runtimeData.coinSpawnRate;

    protected override Vector3 GetRandomPosition()
    {
        bool random = HelperFunctions.GetRandomBool();
        float X = GetRandomPoint(random);
        float Z = GetRandomPoint(!random);
        return new Vector3(X, 0, Z);
    }

    private float GetRandomPoint(bool fromCenter)
    {
        float min = fromCenter ? 0 : minSpawnDistance;
        return Random.Range(min, maxSpawnDistance) * (Random.Range(0, 2) * 2 - 1);
    }
}
