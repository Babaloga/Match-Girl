using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenLoader : MonoBehaviour {

    public string endScreenName;

    public float time;
    public float money;
    public float matches;

    public static EndScreenLoader instance;

    private void Start()
    {
        instance = this;
    }

    public void LoadEndScene()
    {
        time = DayNightCycle.currentTime;
        money = PlayerStatsManager.money;
        matches = PlayerStatsManager.matches;

        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene(endScreenName);
    }
}
