using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret2 : AttackStyle
{
    public override void AttackTarget(TurretBehavior turret)
    {
        GameObject bullet = Resources.Load("Circle", typeof(GameObject)) as GameObject;
        bullet.GetComponent<Bullet>().setTarget(turret.Target);
        bullet.GetComponent<Bullet>().setDamage(turret.Damage());
        Transform.Instantiate(bullet,turret.transform.position,Quaternion.identity);
        soundManager.Instance.sd2Bullet.Play();
    }
}
