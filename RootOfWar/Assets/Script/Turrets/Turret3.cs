using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret3 : AttackStyle
{
    public override void AttackTarget(TurretBehavior turret)
    {
        turret.Target.GetComponent<EnemyBehavior>().getDamage(turret.Damage());
        LineRenderer line = turret.GetComponent<LineRenderer>();
            LineDrawing(line, turret.transform.position, turret.Target.transform.position);
    }

    public void LineDrawing(LineRenderer L, Vector3 StartPos, Vector3 EndPos)
    {
        L.enabled = true;
        L.SetPosition(0, StartPos);
        L.SetPosition(1, EndPos);
    }
}
