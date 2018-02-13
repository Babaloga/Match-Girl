using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {

    const string DIFFICULTY_KEY = "difficulty";
    const string MASTER_VOLUME_KEY = "master_volume";

    public static void setMasterVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Volume out of range");
        }
    }

    public static float getMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    public static void setDifficulty(float difficulty)
    {
        if (difficulty <= 3f && difficulty >= 1f) {
            PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
        }
        else
        {
            Debug.LogError("difficulty out of range");
        }
    }

    public static float getDifficulty()
    {
        return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
    }
    
}
