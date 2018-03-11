using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

	public enum GameState
    {
        playing,
        dialogue,
        menu,
        scripted
    }

    public static GameState state;

    private void Update()
    {
        switch (state)
        {
            case GameState.playing:

                //Normal HUD UI
                //Player has control

                break;

            case GameState.dialogue:

                //Dialogue UI
                //Player is frozen

                break;

            case GameState.menu:

                //Menu UI
                //Player is frozen
                //Game is frozen

                break;

            case GameState.scripted:

                //No UI
                //Objects controlled by script

                break;
        }
    }
}
