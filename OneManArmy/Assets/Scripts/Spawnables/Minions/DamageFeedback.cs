using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFeedback
{
    MeshRenderer mesh;
    Color defaultColor;

    public DamageFeedback(MeshRenderer mesh)
    {
        this.mesh = mesh;
        defaultColor = mesh.material.color;
    }

    public void Blink()
    {
        Sequence s = DOTween.Sequence();
        s.Append(mesh?.material.DOColor(Color.white * 5, 0.1f));
        s.Append(mesh?.material.DOColor(defaultColor, 0.1f));
        s.Play();
    }
}
