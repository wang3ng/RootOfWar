using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet5 : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Target;
    public float speed;
    public float damage;
    public float range;
    public LayerMask EnemyMask;
    private Vector2 actualPosition;
    private float IniDistance;
    private Vector2 TargetPosition;
    public GameObject effect;

    private void Start()
    {
        TargetPosition = Target.position;
        IniDistance = Vector2.Distance(TargetPosition, transform.position);
        actualPosition = transform.position;
    }
    void Update()
    {
        if (Vector2.Distance(TargetPosition, transform.position) < 0.1) 
        {
            Explosion();
            Destroy(gameObject);
        }
        else
        {
            transform.position = actualPosition +
                new Vector2(0, (IniDistance * IniDistance / 4 - Mathf.Pow((Vector2.Distance(TargetPosition, actualPosition) - IniDistance / 2), 2))/3);
            actualPosition = Vector2.MoveTowards(actualPosition, TargetPosition, speed * Time.deltaTime);
            
        }
    }
    public void setTarget(Transform T)
    {
        Target = T;
    }
    public void setDamage(float D)
    {
        damage = D;
    }

    public void Explosion()
    {
        Instantiate(effect,transform.position,Quaternion.identity);
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(transform.position, range, EnemyMask);
        if (HitEnemies != null)
        {
            foreach(Collider2D enemy in HitEnemies)
            {
                enemy.GetComponent<EnemyBehavior>().getDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
