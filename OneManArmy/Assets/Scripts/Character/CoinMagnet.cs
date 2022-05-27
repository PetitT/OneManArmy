using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinMagnet : IUpdatable
{
    private float sqrCoinGrabDistance => Mathf.Pow(DataManager.runtimeData.coinGrabDistance, 2);

    public void OnUpdate()
    {
        foreach (var coin in CoinManager.currentObjects.ToList())
        {
            if (coin.transform.position.sqrMagnitude < sqrCoinGrabDistance)
            {
                coin.gameObject.SetActive(false);
            }
        }
    }
}
