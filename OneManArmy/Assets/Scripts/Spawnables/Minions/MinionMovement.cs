using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : BaseMovement
{
    float speed => DataManager.runtimeData.minionSpeed;

    public MinionMovement(Transform transform) : base(transform) { }

    protected override void Move()
    {
        Vector3 naturalMovement = new Vector3(-transform.position.x, 0, -transform.position.z).normalized;
        Vector3 inputMovement = new Vector3(-currentMovement.x, 0, -currentMovement.y);

        transform.Translate(((speed * naturalMovement) + inputMovement) * Time.deltaTime);
    }
}
