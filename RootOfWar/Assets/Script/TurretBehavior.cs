using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretBehavior : MonoBehaviour
{
    public TurretInfo TurretInfo;
    private float attackCoolDown=0;
    public Transform Target;
    public string State;
    private void Start()
    {
        if (TurretInfo.number == 2) TurretInfo.Behavior = new Turret2();
        transform.Find("Canvas").Find("Number").GetComponent<Text>().text = Mathf.Pow(TurretInfo.number, TurretInfo.power).ToString();
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
        Collider2D[] inRangeEnemies = Physics2D.OverlapCircleAll(transform.position, TurretInfo.range, TurretInfo.enemyMask);
        if(inRangeEnemies.Length > 0)
        {
            Target = inRangeEnemies[0].transform;
            State = "attacking";
        }
    }
    private void attackingTarget()
    {
        if (Vector2.Distance(transform.position, Target.position) <= TurretInfo.range)
        {
            doAttack();
        }
        else State = "hunting";
    }
    public void reducePower(int r)
    {
        Debug.Log(1);
        if(TurretInfo.power%r == 0)
        {
            Debug.Log(2);
            TurretInfo.power /= r;
            transform.Find("Canvas").Find("Number").GetComponent<Text>().text = Mathf.Pow(TurretInfo.number,TurretInfo.power).ToString();
        }
    }
    private void doAttack()
    {
        if(attackCoolDown <= 0)
        {
            TurretInfo.Behavior.attackTarget(this);
            attackCoolDown = TurretInfo.attackTime;
        }
        attackCoolDown -= Time.deltaTime;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, TurretInfo.range);
    }
}
