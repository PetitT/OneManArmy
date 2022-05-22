using LowTeeGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Thrust Attack", menuName = ("Attacks/Thrust Attack"))]
public class MeleeThrust : Attack
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] float cooldown;
    [SerializeField] float range;
    [SerializeField] int lingerInMiliseconds;
    float remainingCooldown;
    GameObject weapon;
    DamageDealer damageDealer;
    Vector3 defaultScale;


    public override void OnInitialize()
    {
        remainingCooldown = cooldown;
        weapon = Instantiate(weaponPrefab, Character.body.transform.position, Quaternion.identity);
        damageDealer = weapon.GetComponentInChildren<DamageDealer>();
        defaultScale = weapon.transform.GetChild(0).localScale;
        weapon.SetActive(false);
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

    private void CheckAttackAvailability()
    {
        Minion closestEnemy = MinionManager.GetClosestMinion();
        if (closestEnemy.transform.position.magnitude < range)
        {
            DoAttack(closestEnemy);
            remainingCooldown = cooldown;
        }
    }

    private async void DoAttack(Minion enemy)
    {
        weapon.transform.LookAt(enemy.transform.position);
        weapon.transform.GetChild(0).localScale = new Vector3(defaultScale.x, defaultScale.y, range);
        damageDealer.SetDamage(damage);
        weapon.SetActive(true);
        await Task.Delay(lingerInMiliseconds);
        if (!Application.isPlaying) return;
        weapon.SetActive(false);
    }
}
