using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret5 : AttackStyle
{
    public override void attackTarget(TurretBehavior turret)
    {
        GameObject bullet = Resources.Load("Bullet5", typeof(GameObject)) as GameObject;
        bullet.GetComponent<Bullet5>().setTarget(turret.Target);
        bullet.GetComponent<Bullet5>().setDamage(turret.TurretInfo.damage);
        Transform.Instantiate(bullet, turret.transform.position, turret.transform.rotation);
    }
}
