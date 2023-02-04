using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform Target;
    public float speed;
    public float damage;
    void Update()
    {
        if (Vector2.Distance(Target.position, transform.position) < 0.1)
        {
            Target.GetComponent<EnemyBehavior>().getDamage(damage);
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
}
