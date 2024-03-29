using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;
    public float[] Timer;
    public int[] EnemyOrder;
    public int waypoint;
    private int current;
    private float clk;
    private int total;
    private void Start()
    {
        current = 0;
        clk = 0;
        total = Mathf.Max(Timer.Length, EnemyOrder.Length);
        if(Timer.Length!= EnemyOrder.Length)
        {
            Debug.Log("Unequal length for the wave");
        }
    }
    void Update()
    {
        if(enemies.Length > 0)
        {
            if (clk >= Timer[current])
            {
                if (EnemyOrder[current] >= enemies.Length)
                {
                    Debug.Log("No enemy of that type provided");
                }
                enemies[EnemyOrder[current]].GetComponent<EnemyBehavior>().lane = waypoint;
                GameObject newEnemy = Instantiate(enemies[EnemyOrder[current]]);
                newEnemy.transform.position = new Vector3(transform.position.x,transform.position.y,-1);
                current += 1;
            }
            clk += Time.deltaTime;
        }
        else
        {
            Debug.Log("No enemy selected");
        }
        if (current >= total)
        {
            //Finish Generating
            Messenger.Broadcast(Events.SponerEnd);
            Destroy(gameObject);
        }
    }
}
