using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : BaseMovement
{
    public CoinMovement(Transform transform) : base(transform) { }

    protected override void Move()
    {
        Vector3 inputMovement = new Vector3(-currentMovement.x, 0, -currentMovement.y);
        transform.Translate(Time.deltaTime * inputMovement);
    }
}
