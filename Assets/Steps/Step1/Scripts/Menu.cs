using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlayHandler(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OnExitHandler()
    {
        Application.Quit();
    }
}
