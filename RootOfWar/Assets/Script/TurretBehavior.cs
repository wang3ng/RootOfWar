using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretBehavior : MonoBehaviour
{
    public AttackStyle Behavior;
    public int number;
    public int power;
    public float damage;
    public float range;
    public float attackTime;
    private float attackCoolDown=0;
    public LayerMask enemyMask;
    public Transform Target;
    public string State;
    private void Start()
    {
        if (number == 2) Behavior = new Turret2();
    }
    // Update is called once per frame
    void Update()
    {
        if(State == "hunting")
        {
            searchingTarget();
        }
        if(State == "attacking")
        {
            attackingTarget();
        }
    }
    private void searchingTarget()
    {
        Collider2D[] inRangeEnemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);
        if(inRangeEnemies.Length > 0)
        {
            Target = inRangeEnemies[0].transform;
            State = "attacking";
        }
    }
    private void attackingTarget()
    {
        if (Vector2.Distance(transform.position, Target.position) <= range)
        {
            doAttack();
        }
        else State = "hunting";
    }
    private void doAttack()
    {
        if(attackCoolDown <= 0)
        {
            Behavior.attackTarget(this);
            attackCoolDown = attackTime;
        }
        attackCoolDown -= Time.deltaTime;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
