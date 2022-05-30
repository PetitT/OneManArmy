using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Thrust Attack", menuName = ("Attacks/Thrust Attack"))]
public class Thrust : AttackChild<ThrustData>
{
    [SerializeField] float handleRange;
    [SerializeField] GameObject weaponPrefab;

    Minion closestMinion;
    float remainingLingerTime;
    GameObject weapon;
    DamageDealer damageDealer;
    Vector3 defaultScale;

    public override void OnInitialize()
    {
        base.OnInitialize();
        weapon = Instantiate(weaponPrefab, Character.body.transform.position, Quaternion.identity);
        damageDealer = weapon.GetComponentInChildren<DamageDealer>();
        defaultScale = weapon.transform.GetChild(0).localScale;
        weapon.SetActive(false);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Timer.CountDown(ref remainingLingerTime, () => weapon.SetActive(false));
    }

    protected override void OnLevelUp()
    {
        ThrustData levelUpData = dataList[currentLevel - 1];
        data.Damage += levelUpData.Damage;
        data.Cooldown += levelUpData.Cooldown;
        data.Range += levelUpData.Range;
        data.Linger += levelUpData.Linger;
    }

    protected override bool IsAttackAvailable()
    {
        closestMinion = MinionManager.GetClosestMinion();
        if (closestMinion == null) return false;
        if (closestMinion.transform.position.sqrMagnitude < Mathf.Pow((data.Range + handleRange), 2))
        {
            return true;
        }
        return false;
    }

    protected override void DoAttack()
    {
        weapon.SetActive(false);
        weapon.transform.LookAt(new Vector3(closestMinion.transform.position.x, weapon.transform.position.y, closestMinion.transform.position.z));
        weapon.transform.GetChild(0).localScale = new Vector3(defaultScale.x, defaultScale.y, data.Range);
        damageDealer.SetDamage(data.Damage);
        weapon.SetActive(true);
        remainingLingerTime = data.Linger;
    }
}

[Serializable]
public class ThrustData : AttackData
{
    public float Damage;
    public float Range;
    public float Linger;
}
