using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    public static GameState state;

    public PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        state = GameState.playing;
    }

    private void Update()
    {
        print(state);

        switch (state)
        {
            case GameState.playing:

                //Normal HUD UI
                //Player has control

                playerMovement.enabled = true;
                Time.timeScale = 1;

                break;

            case GameState.dialogue:

                //Dialogue UI
                //Player is frozen

                playerMovement.enabled = false;
                Time.timeScale = 1;

                break;

            case GameState.menu:

                //Menu UI
                //Player is frozen
                //Game is frozen

                playerMovement.enabled = false;
                Time.timeScale = 0.001f;

                break;

            case GameState.scripted:

                //No UI
                //Objects controlled by script

                playerMovement.enabled = false;
                Time.timeScale = 1;

                break;

            case GameState.dead:

                //Game is frozen
                //Game over UI

                playerMovement.enabled = false;
                Time.timeScale = 0.001f;

                DeathUI.ShowDeathUI();

                break;
        }
    }
}

public enum GameState
{
    playing,
    dialogue,
    menu,
    scripted,
    dead
}
