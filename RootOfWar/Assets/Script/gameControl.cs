using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameControl : MonoBehaviour
{
    public int totalSpawner;
    // Player state
    public string state;
    // The Turret holded if selected
    private InventuryTurret info;
    // The root gun power if selected 
    public int root;
    private void OnEnable()
    {
        Messenger.AddListener(Events.SponerEnd, OneSponerEnd);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener(Events.SponerEnd, OneSponerEnd);
    }
    private void OneSponerEnd()
    {
        totalSpawner -= 1;
    }
    void Start()
    {
        Time.timeScale = 1;
        state = "";
    }
    private void Update()
    {
        if (totalSpawner <= 0)
        {
            if(FindObjectOfType<EnemyBehavior>() == null)
            {
                Messenger.Broadcast(Events.Levelcomplete);
            }
        }
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
        if(getState()=="turret" && info != null) return info.Turret;
        else return null;
    }
}
