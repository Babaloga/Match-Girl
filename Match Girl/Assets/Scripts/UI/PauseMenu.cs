﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool isPaused = false;
    public static bool disablePause = false;
    public GameObject pauseMenu; 

	// Update is called once per frame
	void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == true)
            {
                resume();
            }
            else
            {

                pause();

            }
        }   
	}

    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void loadMenu()
    {

        SceneManager.LoadScene("Start Menu");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}