using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    Health health;
    MinionMovement movement;

    List<IUpdatable> updatables;

    private void Start()
    {
        health = new Health(10);
        movement = new MinionMovement(transform);

        updatables.Add(movement);
    }

    private void Update()
    {
        updatables.ForEach(t => t.OnUpdate());
    }

}
