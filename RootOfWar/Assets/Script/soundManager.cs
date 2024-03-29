using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static soundManager Instance;

    public AudioSource sd2Bullet;
    public AudioSource sd3Laser;
    public AudioSource sd5BombShoot;
    public AudioSource sd5BombHit;
    public AudioSource sdWin;
    public AudioSource sdFail;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void playSound(AudioSource sdFx)
    {

        sdFx.Play();
    }



}
