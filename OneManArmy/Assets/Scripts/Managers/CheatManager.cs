using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    public Attack attackToAdd;
    public void AddAttack()
    {
        CombatManager.AddAttack(attackToAdd);
    }

    public void ClearAllMinions()
    {
        foreach (var minion in FindObjectsOfType<Minion>())
        {
            minion.gameObject.SetActive(false);
            MinionManager.RemoveMinion(minion);
        }
    }
}
