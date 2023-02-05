using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform[] waypoints;
    public int lane;
    private int position;
    public float speed=10;
    public float health=100;
    public float freeze_efficient = 0f;
    public float slow_speed;
    // Start is called before the first frame update
    public void slow(float amount)
    {
        slow_speed = amount;
    }
    void Start()
    {
        position = 1;
        string waypoint = "Waypoints" + lane.ToString();
        GameObject p = GameObject.Find(waypoint);
        waypoints = p.GetComponentsInChildren<Transform>();
        transform.right = waypoints[position].position - transform.position;
        slow_speed = speed * freeze_efficient;
        Debug.Log(freeze_efficient);
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
            if (transform.position == waypoints[position].position && position+1<waypoints.Length)
            {
                position += 1;
                //Vector3 dir = waypoints[position].position - transform.position;
                //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.right = waypoints[position].position - transform.position;
            }
        }
    }
    public void getDamage(float damage)
    {
        health -= damage;
        //Hurt animation
        if(health < 0)
        {
            //Death Animation
            Destroy(gameObject);
        }
    }
}
