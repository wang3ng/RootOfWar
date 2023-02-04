using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameControl : MonoBehaviour
{
    public string state;
    private InventuryTurret info;
    public int root;
    void Start()
    {
        state = "";
    }
    public void RootPrepare(int newRoot)
    {
        state = "root";
        root = newRoot;
    }
    public void TurretPrepare(InventuryTurret turret)
    {
        state = "turret";
        info = turret;
    }
    public void useInventory()
    {
        Destroy(info.gameObject);
    }
    public GameObject placeTurret()
    {
        if(info == null) return null;
        else return info.Turret;
    }
}
