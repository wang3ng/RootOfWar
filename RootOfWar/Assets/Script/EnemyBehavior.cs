using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform[] waypoints;
    private int position;
    public float speed=10;
    public float health=100;
    // Start is called before the first frame update
    void Start()
    {
        position = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if(waypoints.Length > 0 && position < waypoints.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[position].position, speed * Time.deltaTime);
            if (transform.position == waypoints[position].position) position += 1;
        }
    }
    public void getDamage(float damage)
    {
        health -= damage;
        if(health < 0)
        {
            //Death Animation
            Destroy(gameObject);
        }
    }
}
