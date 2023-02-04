using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventuryTurret : MonoBehaviour
{
    public GameObject Turret;
    public gameControl gameController;
    private void Start()
    {
        gameController = GameObject.Find("GameSatus").GetComponent<gameControl>();
        GetComponent<Button>().onClick.AddListener(clicking);
    }
    public void clicking()
    {
        if (gameController.state == "root")
        {
            if(Turret.GetComponent<TurretBehavior>().power > 1)
            {
                Turret.GetComponent<TurretBehavior>().power -= gameController.root;
            }
        }
        else
        {
            gameController.TurretPrepare(this);
        }
    }
}
