using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * This class is for the turret holders on the map. 
 * Either allow players to put turret on them or 
 * shoot the turret in placeholder with the root gun.
 **/
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
        turretInplace = gameController.placeTurret();
        if (turretInplace != null)
        {
            //turretInplace = Instantiate(turret, transform.position, Quaternion.identity);
            turretInplace.SetActive(true);
            turretInplace.transform.position = transform.position;
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
            Instantiate(Resources.Load("Object/Turret/TurretInventury", typeof(GameObject)) 
                as GameObject, GameObject.Find("Inventory").transform).GetComponent<InventuryTurret>().Turret=Instantiate(turretInplace);
            /**if (invT != null)
            {
                Debug.Log(invT.GetComponent<TurretBehavior>());
                invT.GetComponent<InventuryTurret>().Turret.GetComponent<TurretBehavior>().TurretInfo
                    =turretInplace.GetComponent<TurretBehavior>().TurretInfo;
            }**/
        }
    }
    void Update()
    {
        
    }
}
