using LowTeeGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Transform body;

    private void Start()
    {
        body = transform;
    }
}
