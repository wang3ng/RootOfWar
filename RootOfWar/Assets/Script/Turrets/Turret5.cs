using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret5 : AttackStyle
{
    public override void AttackTarget(TurretBehavior turret)
    {
        GameObject bullet = Resources.Load("Bullet5", typeof(GameObject)) as GameObject;
        bullet.GetComponent<Bullet5>().setTarget(turret.Target);
        bullet.GetComponent<Bullet5>().setDamage(turret.Damage());
        Transform.Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        soundManager.Instance.sd5BombShoot.Play();
    }
}
