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


    public override void OnInitialize()
    {
        remainingCooldown = cooldown;
        weapon = Instantiate(weaponPrefab, Character.body);
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
        GameObject closestEnemy = EnemyManager.GetClosestEnemy();
        if (closestEnemy.transform.position.magnitude < range)
        {
            DoAttack(closestEnemy);
            remainingCooldown = cooldown;
        }
    }

    private async void DoAttack(GameObject enemy)
    {
        enemy.SetActive(false);
        weapon.transform.LookAt(enemy.transform.position);
        Vector3 currentScale = weaponPrefab.transform.GetChild(0).localScale;
        weapon.transform.GetChild(0).localScale = new Vector3(currentScale.x, currentScale.y, range);
        weapon.SetActive(true);
        await Task.Delay(lingerInMiliseconds);
        weapon.SetActive(false);
    }


}
