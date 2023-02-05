using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This object would provide information for the turret. 
 * In order to make a new turret, you would need to create 
 * a scriptable object in unity and assign information to it
 **/
[CreateAssetMenu(fileName = "newTurret", menuName = "turret")]
public class TurretInfo : ScriptableObject
{
    [SerializeField]
    public AttackStyle Behavior;
    public int number;
    public int power;
    public float range;
    public float attackTime;
    public LayerMask enemyMask;
}
