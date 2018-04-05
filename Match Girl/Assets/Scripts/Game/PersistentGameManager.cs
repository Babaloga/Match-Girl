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
    private static float averageTemperature;
    private float temperatureSum;
    private float temperatureMeasurements;

    public static PlayerStats persistentStats;

    public float dayHungerPenalty = 34;

    public static PersistentGameManager instance;
    public Fader sceneFader;

    public static bool debugMode = false;
    private bool recordTemperature = true;

    public static EffectLevel persistentSicknessLevel;

    private void Start()
    {
        instance = this;
        persistentSicknessLevel = EffectLevel.None;
		persistentStats.boots = false;
        if (!debugMode)
            StartCoroutine(LoadSceneSetActive(mainSceneName));
        else
            sceneFader.FadeOut();
    }

    private void FixedUpdate()
    {
        if (recordTemperature)
        {
            if (Time.frameCount % 30 == 0)
            {
                temperatureSum += PlayerStatsManager.Warmth;
                temperatureMeasurements++;
            }
        }
    }

    public void LoadEndScene()
    {
        recordTemperature = false;
        averageTemperature = temperatureSum / temperatureMeasurements;
        time = DayNightCycle.currentTime;
        persistentStats = PlayerStatsManager.stats;
        persistentStats.food -= dayHungerPenalty;
        StartCoroutine(FadeAndSwitchScenes(endScreenName));
    }

    public void LoadMainScene()
    {
        currentDay++;
        persistentStats.food = ResourceManager.variableFood;
        DetermineSickness();
        StartCoroutine(FadeAndSwitchScenes(mainSceneName));
        temperatureSum = 0;
        temperatureMeasurements = 0;
        recordTemperature = true;
    }

    public void LoadFirstDay()
    {
        SceneManager.LoadScene("Persistent", LoadSceneMode.Single);
    }

    public void ReloadCurrentDay()
    {
        StartCoroutine(FadeAndSwitchScenes(mainSceneName));
    }

    private void DetermineSickness()
    {
        float food = persistentStats.food;
        float voice = persistentStats.callStrength;

        float sicknessValue = ((100f - averageTemperature) + (100f * (1f - voice))) / (50f + (food / 2f));

        int sicknessInt = Mathf.RoundToInt(sicknessValue) - 1;

        if ((int)persistentSicknessLevel + sicknessInt > 3)
        {
            //Consequence for exceeding sickness level
            persistentSicknessLevel = EffectLevel.High;
        }
        else if((int)persistentSicknessLevel + sicknessInt < 0)
        {
            persistentSicknessLevel = EffectLevel.None;
        }
        else
        {
            persistentSicknessLevel += sicknessInt;
        }      
    }

    private IEnumerator FadeAndSwitchScenes(string sceneName)
    {
        sceneFader.FadeIn();

        while (sceneFader.fader.alpha < 1) yield return null;

        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        StartCoroutine(LoadSceneSetActive(sceneName));
    }

    private IEnumerator LoadSceneSetActive(string sceneName)
    {
        print("Load Scene Set Active Called");

        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadedScene);

        sceneFader.FadeOut();
    }
}
