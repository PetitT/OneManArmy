using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Thrust Attack", menuName = ("Attacks/Thrust Attack"))]
public class Thrust : Attack
{
    [SerializeField] float handleRange;
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] List<ThrustData> dataList;

    ThrustData data;
    float remainingCooldown;
    float remainingLingerTime;
    GameObject weapon;
    DamageDealer damageDealer;
    Vector3 defaultScale;

    public override void OnInitialize()
    {
        remainingCooldown = data.cooldown;
        weapon = Instantiate(weaponPrefab, Character.body.transform.position, Quaternion.identity);
        damageDealer = weapon.GetComponentInChildren<DamageDealer>();
        defaultScale = weapon.transform.GetChild(0).localScale;
        weapon.SetActive(false);
        data.CopyFieldsFrom(dataList[0]);
        currentLevel = 1;
    }

    public override void OnUpdate()
    {
        if (remainingCooldown > 0)
        {
            remainingCooldown -= Time.deltaTime;
        }
        else
        {
            CheckAttackAvailability();
        }

        Timer.CountDown(ref remainingLingerTime, () => weapon.SetActive(false));
    }

    protected override void OnLevelUp()
    {
        ThrustData levelUpData = dataList[currentLevel - 1];
        data.damage += levelUpData.damage;
        data.cooldown += levelUpData.cooldown;
        data.range += levelUpData.range;
        data.linger += levelUpData.linger;
    }

    public override string GetCurrentLevelUpInfo()
    {
        return dataList[currentLevel].info;
    }

    private void CheckAttackAvailability()
    {
        Minion closestEnemy = MinionManager.GetClosestMinion();
        if (closestEnemy == null) return;
        if (closestEnemy.transform.position.magnitude < data.range + handleRange)
        {
            DoAttack(closestEnemy);
            remainingCooldown = data.cooldown;
        }
    }

    private void DoAttack(Minion enemy)
    {
        weapon.SetActive(false);
        weapon.transform.LookAt(new Vector3(enemy.transform.position.x, weapon.transform.position.y, enemy.transform.position.z));
        weapon.transform.GetChild(0).localScale = new Vector3(defaultScale.x, defaultScale.y, data.range);
        damageDealer.SetDamage(data.damage);
        weapon.SetActive(true);
        remainingLingerTime = data.linger;
    }
}

[Serializable]
public class ThrustData
{
    public string info;
    public float damage;
    public float cooldown;
    public float range;
    public float linger;
}
