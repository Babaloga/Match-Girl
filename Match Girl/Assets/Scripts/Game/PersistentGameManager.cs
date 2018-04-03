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

    public static PlayerStats persistentStats;

    public float dayHungerPenalty = 34;

    public static PersistentGameManager instance;

    public bool debugMode = false;

    public static EffectLevel persistentSicknessLevel;

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        persistentSicknessLevel = EffectLevel.None;
    }

    public void LoadEndScene()
    {
        time = DayNightCycle.currentTime;
        persistentStats = PlayerStatsManager.stats;
        persistentStats.food -= dayHungerPenalty;
        SceneManager.LoadScene(endScreenName);
    }

    public void LoadMainScene()
    {
        currentDay++;
        persistentStats.food = ResourceManager.variableFood;
        SceneManager.LoadScene(mainSceneName);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Start Menu") Destroy(gameObject);
    }
}
