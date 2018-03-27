using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentGameManager : MonoBehaviour {

    public static int currentDay = 1;
    public int totalDays = 7;

    public string endScreenName;
    public string mainSceneName;

    public static float time;
    public static int money;
    public static int matches;
    public static float hunger;

    public float dayHungerPenalty = 34;

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
        hunger += dayHungerPenalty;
        SceneManager.LoadScene(endScreenName);
    }

    public void LoadMainScene()
    {
        currentDay++;
        hunger = ResourceManager.variableHunger;
        SceneManager.LoadScene(mainSceneName);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Start Menu") Destroy(gameObject);
    }
}
