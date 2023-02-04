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
        transform.Find("Number").GetComponent<Text>().text = Mathf.Pow(Turret.GetComponent<TurretBehavior>().number, Turret.GetComponent<TurretBehavior>().power).ToString();
        gameController = GameObject.Find("GameSatus").GetComponent<gameControl>();
        GetComponent<Button>().onClick.AddListener(clicking);
    }
    public void clicking()
    {
        // If the player selected the root gun, Root the tower
        if (gameController.state == "root")
        {
            // Check for the power of the turret to see if avaliable to root
            int p = Turret.GetComponent<TurretBehavior>().power;
            if (p % gameController.root == 0)
            {
                // if rootable, root and copy object
                p /= gameController.root;
                Debug.Log(p);
                transform.Find("Number").GetComponent<Text>().text = Mathf.Pow(Turret.GetComponent<TurretBehavior>().number, p).ToString();
                GameObject newInv = Instantiate(gameObject,transform.parent);
            }
            gameController.state = "";
        }
        else
        {
            // Select the inventury turret
            gameController.TurretPrepare(this);
        }
    }
}
