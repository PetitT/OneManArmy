using LowTeeGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : IUpdatable
{
    protected abstract GameObject objectToSpawn { get; }
    protected abstract float spawnDelay { get; }
    protected Vector2 movement => MovementManager.movement;
    protected float minSpawnDistance => DataManager.runtimeData.minSpawnDistance;
    protected float maxSpawnDistance => DataManager.runtimeData.maxSpawnDistance;

    float remainingSpawnDelay;

    public Spawner()
    {
        remainingSpawnDelay = spawnDelay;
    }

    public void OnUpdate()
    {
        Timer.LoopedCountDown(ref remainingSpawnDelay, spawnDelay, Spawn);
    }

    private void Spawn()
    {
        Pool.Instance.GetItemFromPool(objectToSpawn, GetRandomPosition(), Quaternion.identity);
    }

    protected abstract Vector3 GetRandomPosition();
}
