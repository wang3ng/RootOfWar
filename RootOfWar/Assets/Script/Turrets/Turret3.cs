using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret3 : AttackStyle
{
    public float freeze_efficient = 0.9f;
    public override void AttackTarget(TurretBehavior turret)
    {
        Debug.Log("1");
        float damage = turret.Damage() * 0.14f;
        turret.Target.GetComponent<EnemyBehavior>().getDamage(damage);
        LineRenderer line = turret.GetComponent<LineRenderer>();
        LineDrawing(line, turret.transform.position, turret.Target.transform.position);
        turret.Target.GetComponent<EnemyBehavior>().slow(turret.TurretInfo.power);
        soundManager.Instance.sd3Laser.Play();
    }

    public void LineDrawing(LineRenderer L, Vector3 StartPos, Vector3 EndPos)
    {
        L.enabled = true;
        L.SetPosition(0, StartPos);
        L.SetPosition(1, EndPos);
    }
}
