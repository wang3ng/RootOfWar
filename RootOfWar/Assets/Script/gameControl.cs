using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameControl : MonoBehaviour
{
    // Player state
    public string state;
    // The Turret holded if selected
    private InventuryTurret info;
    // The root gun power if selected 
    public int root;
    void Start()
    {
        state = "";
    }
    public string getState()
    {
        string cp = string.Copy(state);
        state = "";
        return cp;
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
