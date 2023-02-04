using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretPlacing : MonoBehaviour
{
    public gameControl gameController;
    private GameObject turretInplace;
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
            turretInplace = Instantiate(turret, transform.position, Quaternion.identity);
            gameController.useInventory();
            GetComponent<Button>().onClick.RemoveAllListeners();
            GetComponent<Button>().onClick.AddListener(rootTurret);
        }
    }
    private void rootTurret()
    {
        if(gameController.getState() == "root")
        {
            turretInplace.GetComponent<TurretBehavior>().reducePower(gameController.root);
            GameObject invT = Instantiate(Resources.Load("Object/Turret/TurretInventury", typeof(GameObject)) 
                as GameObject, GameObject.Find("Inventory").transform);
            if (invT != null)
            {
                invT.GetComponent<InventuryTurret>().Turret.GetComponent<TurretBehavior>().TurretInfo
                    = turretInplace.GetComponent<TurretBehavior>().TurretInfo;
            }
        }
    }
    void Update()
    {
        
    }
}
