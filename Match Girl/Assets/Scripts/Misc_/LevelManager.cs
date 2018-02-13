using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public float invokeNextLevel;

    public void Start()
    {
        if (invokeNextLevel == 0)
        {

        }
        else
        {
            Invoke("nextLevel", invokeNextLevel);
        }
    }
    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
