using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newTurret", menuName = "turret")]
public class TurretInfo : ScriptableObject
{
    [SerializeField]
    public AttackStyle Behavior;
    public int number;
    public int power;
    public float damage;
    public float range;
    public float attackTime;
    public LayerMask enemyMask;
}
