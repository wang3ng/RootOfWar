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
    private float currentSpeed;
    // Start is called before the first frame update
    public void slow(float amount)
    {
        currentSpeed=speed*(Mathf.Pow(0.75f,amount));
        effectClock[0] = 1f;
    }
    void Start()
    {
        effectClock = new float[1];
        position = 1;
        string waypoint = "Waypoints" + lane.ToString();
        GameObject p = GameObject.Find(waypoint);
        waypoints = p.GetComponentsInChildren<Transform>();
        transform.right = waypoints[position].position - transform.position;
        currentSpeed = speed;
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < effectClock.Length; i++)
        {
            effectClock[i] -= Time.deltaTime;
            if (effectClock[i] <= 0)
            {
                clearEffect(i);
            }
        }
        effectClock[0] -= Time.deltaTime;
        Move();
    }
    void Move()
    {
        if(waypoints.Length > 0 && position < waypoints.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[position].position, currentSpeed * Time.deltaTime);
            if (transform.position == waypoints[position].position && position+1<waypoints.Length)
            {
                position += 1;
                //Vector3 dir = waypoints[position].position - transform.position;
                //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.right = waypoints[position].position - transform.position;
            }
        }
        if(position+1 >= waypoints.Length){
            Debug.Log('a');
            Messenger.Broadcast(Events.Damage);
            Destroy(gameObject);
        }
    }
    public float[] effectClock;
    private void clearEffect(int i)
    {
        if (i == 0)
        {
            currentSpeed = speed;
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
