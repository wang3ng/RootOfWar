using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    private void Start()
    {
        health = 5;
    }
    private void OnEnable()
    {
        Messenger.AddListener(Events.Damage, onHurt);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener(Events.Damage, onHurt);
    }
    private void onHurt()
    {
        health--;
        string t = transform.Find("HealthBar").GetComponent<Text>().text;
        if(t.Length > 0)
        {
            t = t.Remove(t.Length - 1);
            transform.Find("HealthBar").GetComponent<Text>().text = t;
        }
        if (health <= 0)
        {
            soundManager.Instance.sdFail.Play();
            transform.Find("EndScene").gameObject.SetActive(true);
            Time.timeScale = 0;
            
        }
    }
}
