using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomText : MonoBehaviour
{
    public Sprite[] words;
    void Start()
    {
        GetComponent<Image>().sprite = words[Random.Range(0, words.Length)];
    }

    void Update()
    {
        
    }
}
