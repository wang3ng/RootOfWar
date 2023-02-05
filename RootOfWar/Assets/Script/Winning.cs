using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winning : MonoBehaviour
{
    private void OnEnable()
    {
        Messenger.AddListener(Events.Levelcomplete, winning);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener(Events.Levelcomplete, winning);
    }
    private void winning()
    {
        transform.Find("WinScene").gameObject.SetActive(true);
        Messenger.RemoveListener(Events.Levelcomplete, winning);
    }
}
