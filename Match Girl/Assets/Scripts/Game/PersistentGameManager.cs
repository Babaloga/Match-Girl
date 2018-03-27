using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentGameManager : MonoBehaviour {

    public int currentDay = 1;
    public int totalDays = 7;

    public string endScreenName;
    public string mainSceneName;

    public static float time;
    public static int money;
    public static int matches;
    public static float hunger;

    public static PersistentGameManager instance;

    private void Start()
    {
        if (!instance) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void LoadEndScene()
    {
        time = DayNightCycle.currentTime;
        money = PlayerStatsManager.money;
        matches = PlayerStatsManager.matches;
        hunger = PlayerStatsManager.hunger;
        SceneManager.LoadScene(endScreenName);
    }

    public void LoadMainScene()
    {
        currentDay++;
        SceneManager.LoadScene(mainSceneName);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Start Menu") Destroy(gameObject);
    }
}
