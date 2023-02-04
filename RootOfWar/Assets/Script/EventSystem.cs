using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public string state;
    private GameObject info;
    private void Start()
    {
        state = "";
    }
    public void changeState(string newState, GameObject information)
    {
        state = newState;
        info = information;
    }
}
