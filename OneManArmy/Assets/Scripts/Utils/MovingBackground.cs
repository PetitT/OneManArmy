using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : IUpdatable
{
    private Material mat;
    private Vector2 direction => MovementManager.movement * DataManager.runtimeData.groundTextureSpeedMultiplicator;

    public MovingBackground(MeshRenderer background)
    {
        mat = background.material;
    }

    void IUpdatable.OnUpdate()
    {
        MoveTexture();
    }

    private void MoveTexture()
    {
        Vector2 current = mat.GetTextureOffset("_MainTex");
        mat.SetTextureOffset("_MainTex", current + direction * Time.deltaTime);
    }

}
