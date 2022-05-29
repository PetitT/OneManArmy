using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinMagnet : IUpdatable
{
    private float sqrCoinGrabDistance => Mathf.Pow(DataManager.runtimeData.coinGrabDistance, 2);

    public void OnUpdate()
    {
        foreach (var coin in CoinManager.CurrentObjects.ToList())
        {
            if (coin.transform.position.sqrMagnitude < sqrCoinGrabDistance)
            {
                OnCoinCollectedEvent onCoinCollectedEvent = new OnCoinCollectedEvent();
                onCoinCollectedEvent.FireEvent();
                coin.gameObject.SetActive(false);
            }
        }
    }
}
