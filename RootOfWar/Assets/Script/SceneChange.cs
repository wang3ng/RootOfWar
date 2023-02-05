using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int currentScene;
    public void ChangeScene(int i)
    {
        SceneManager.LoadScene("Level"+i.ToString());
        currentScene = i;
    }
    public void Reload()
    {
        ChangeScene(currentScene);
    }
}
