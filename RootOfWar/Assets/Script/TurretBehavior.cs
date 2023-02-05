using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * This class control the behavior of a turret in the game scene.
 **/
public class TurretBehavior : MonoBehaviour
{
    public TurretInfo TurretInfo;
    // Time the attack cool down
    private float attackCoolDown=0;
    // Record the target
    public Transform Target;
    // The state of the turret, which can be "hunting" and "attacking", each remark searching enemy and shooting enenmy
    public string State;

    public bool drawable = false;
    private void Start()
    {
        // Make a copy of the turretinfo to avoid changing the information in the object.
        TurretInfo = Instantiate(TurretInfo);

        // This section would assign the correct behavior for the turret
        if (TurretInfo.number == 2) TurretInfo.Behavior = new Turret2();
        if (TurretInfo.number == 5) TurretInfo.Behavior = new Turret5();
        if (TurretInfo.number == 3) TurretInfo.Behavior = new Turret3();


        // Change the number that represents the turret.
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
    /**
     * This function would search for the enemy within radius.
     **/
    private void searchingTarget()
    {
        Collider2D[] inRangeEnemies = Physics2D.OverlapCircleAll(transform.position, TurretInfo.range, TurretInfo.enemyMask);
        if (inRangeEnemies.Length > 0)
        {
            Target = inRangeEnemies[0].transform;
            State = "attacking";
        }
        else if (GetComponent<LineRenderer>()) GetComponent<LineRenderer>().enabled = false;
    }
    /**
     * This function would do the attack assigned to the turret in the attack speed given.
     **/
    private void attackingTarget()
    {
        if (Target!=null && Vector2.Distance(transform.position, Target.position) <= TurretInfo.range)
        {
            if (Target.position.x <= transform.position.x)
            {
                transform.Find("Head").right = transform.position - Target.position;
                transform.Find("Head").localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.Find("Head").right = -transform.position + Target.position;
                transform.Find("Head").localScale = new Vector3(1, 1, 1);
            }
            
            doAttack();
        }
        else State = "hunting";
    }
    private void doAttack()
    {
        if (attackCoolDown <= 0)
        {
            attackCoolDown = TurretInfo.attackTime;
            TurretInfo.Behavior.AttackTarget(this);            
        }
        attackCoolDown -= Time.deltaTime;
    }
    /**
     * Input: the root power.
     * The function would reduce the turret's number rooting with r.
     **/
    public void reducePower(int r)
    {
        if(TurretInfo.power%r == 0)
        {
            TurretInfo.power /= r;
            transform.Find("Canvas").Find("Number").GetComponent<Text>().text = Mathf.Pow(TurretInfo.number,TurretInfo.power).ToString();
        }
    }
    /**
     * 
     **/
    public float Damage()
    {
        float damage = 70 / (1 + 5 * (Mathf.Exp(0.8f - TurretInfo.power)));
        return damage;
    }
    /**
     * For editing purpose
     **/
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, TurretInfo.range);
    }
}
