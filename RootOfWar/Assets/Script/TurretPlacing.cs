using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretPlacing : MonoBehaviour
{
    public gameControl gameController;
    private void Start()
    {
        gameController = GameObject.Find("GameSatus").GetComponent<gameControl>();
        GetComponent<Button>().onClick.AddListener(placeTurret);
    }
    private void placeTurret()
    {
        GameObject turret = gameController.placeTurret();
        if (turret != null)
        {
            Instantiate(turret, transform.position, Quaternion.identity);
            gameController.useInventory();
        }
    }
    void Update()
    {
        
    }
}
