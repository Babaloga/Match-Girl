using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public float invokeNextLevel;

    public void Start()
    {
        if (invokeNextLevel != 0)
        {
            Invoke("nextLevel", invokeNextLevel);
        }
    }
    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void ReloadCurrent()
    {
        print("Reload Current");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void RestartDay()
    {
        PersistentGameManager.instance.LoadFirstDay();
    }

    public void nextLevel()
    {
        print("Next Level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
