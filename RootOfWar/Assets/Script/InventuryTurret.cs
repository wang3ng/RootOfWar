using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventuryTurret : MonoBehaviour
{
    // The turret holded in inventory, assigned in Unity
    public GameObject Turret;
    public gameControl gameController;
    private void Start()
    {
        transform.Find("Number").GetComponent<Text>().text = Mathf.Pow(Turret.GetComponent<TurretBehavior>().TurretInfo.number, 
            Turret.GetComponent<TurretBehavior>().TurretInfo.power).ToString();
        gameController = GameObject.Find("GameSatus").GetComponent<gameControl>();
        GetComponent<Button>().onClick.AddListener(clicking);
    }
    public void clicking()
    {
        // If the player selected the root gun, Root the tower
        if (gameController.getState() == "root")
        {
            // Check for the power of the turret to see if avaliable to root
            TurretBehavior tb = Turret.GetComponent<TurretBehavior>();
            if (tb.TurretInfo.power % gameController.root == 0)
            {
                // if rootable, root and copy object
                tb.TurretInfo.power /= gameController.root;
                transform.Find("Number").GetComponent<Text>().text = Mathf.Pow(tb.TurretInfo.number, tb.TurretInfo.power).ToString();
                GameObject newInv = Instantiate(gameObject,transform.parent);
            }
        }
        else
        {
            // Select the inventury turret
            gameController.TurretPrepare(this);
        }
    }
}
