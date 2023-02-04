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
    void Update()
    {
        if (Vector2.Distance(Target.position, transform.position) < 0.1)
        {
            Explosion();
            Destroy(gameObject);
        }
        else transform.position = Vector2.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
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
