using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public string sceneName; // Имя сцены, которую нужно активировать

    public void ActivateScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
