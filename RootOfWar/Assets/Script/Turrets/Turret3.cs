using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret3 : AttackStyle
{
    public override void attackTarget(TurretBehavior turret)
    {
        turret.Target.GetComponent<EnemyBehavior>().getDamage(turret.TurretInfo.damage);
        LineRenderer line = turret.GetComponent<LineRenderer>();
        line.enabled = true;
        line.SetPosition(0, turret.transform.position);
        line.SetPosition(1, turret.Target.transform.position);
    }
}
