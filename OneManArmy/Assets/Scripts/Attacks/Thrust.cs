using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Thrust Attack", menuName = ("Attacks/Thrust Attack"))]
public class Thrust : Attack
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] List<ThrustData> dataList;

    ThrustData data;
    float remainingCooldown;
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
    }

    protected override void OnLevelUp()
    {
        ThrustData levelUpData = dataList[currentLevel - 1];
        data.damage += levelUpData.damage;
        data.cooldown += levelUpData.cooldown;
        data.range += levelUpData.range;
        data.lingerInMs += levelUpData.lingerInMs;
    }

    private void CheckAttackAvailability()
    {
        Minion closestEnemy = MinionManager.GetClosestMinion();
        if (closestEnemy.transform.position.magnitude < data.range)
        {
            DoAttack(closestEnemy);
            remainingCooldown = data.cooldown;
        }
    }

    private async void DoAttack(Minion enemy)
    {
        weapon.transform.LookAt(enemy.transform.position);
        weapon.transform.GetChild(0).localScale = new Vector3(defaultScale.x, defaultScale.y, data.range);
        damageDealer.SetDamage(data.damage);
        weapon.SetActive(true);
        await Task.Delay(data.lingerInMs);
        if (!Application.isPlaying) return;
        weapon.SetActive(false);
    }
}

[Serializable]
public class ThrustData
{
    public string info;
    public float damage;
    public float cooldown;
    public float range;
    public int lingerInMs;
}
