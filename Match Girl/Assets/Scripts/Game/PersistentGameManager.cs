﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentGameManager : MonoBehaviour {

    public static bool bro_alive = true;
    public static bool bro_fed = false;
    public static bool bro_medicated = false;
    public static SicknessLevel broSickness;
    public static HungerLevel broHunger;

    public static bool sis_alive = true;
    public static bool sis_fed = false;
    public static bool sis_medicated = false;
    public static SicknessLevel sisSickness;
    public static HungerLevel sisHunger;

    public static bool father_alive = true;
    public static bool father_fed = false;
    public static bool father_medicated = false;
    public static bool father_bandaged = true;
    public static bool father_bandaged_previous = false;
    public static SicknessLevel dadSickness;
    public static HungerLevel dadHunger;

    public static int currentDay = 0;
    public int totalDays = 7;

    public string endScreenName;
    public string mainSceneName;
    public string intermediateSceneName;
    public string finalSceneName;

    public static float time;
    private static float averageTemperature;
    private float temperatureSum;
    private float temperatureMeasurements;

    public static PlayerStats persistentStats;
    public PlayerStats debugStats;

    public float dayHungerPenalty = 34;

    public static PersistentGameManager instance;
    public Fader sceneFader;

    public static bool debugMode = false;
    public bool localDebug = false;
    private bool recordTemperature = true;

    public static EffectLevel persistentSicknessLevel;

    private void Start()
    {
        
        debugMode = localDebug;
        if(!instance)
            instance = this;
        else
        {
            enabled = false;
            return;
        }
        persistentSicknessLevel = EffectLevel.None;
		persistentStats.boots = false;
        if (!debugMode)
            StartCoroutine(LoadSceneSetActive(intermediateSceneName));
        else
        {
            if(sceneFader) sceneFader.FadeOut();
            persistentStats = debugStats;
        }
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

    public void LoadGameEnd() {

        foreach (FamilyResources f in FindObjectsOfType<FamilyResources>())
        {
            if (!f.isPlayer)
            {
                f.DetermineHunger();
                f.DetermineSickness();
            }
        }

        StartCoroutine(FadeAndSwitchScenes(finalSceneName));

    }

    public void LoadIntermediateScene()
    {
        AllConditions.FindCondition("SisterDead").satisfied = !sis_alive;
        AllConditions.FindCondition("BrotherDead").satisfied = !bro_alive;
        AllConditions.FindCondition("FatherDead").satisfied = !father_alive;

        if (currentDay == 7)
        {
            LoadGameEnd();
            return;
        }
        currentDay++;
        print(currentDay);
        persistentStats.food = ResourceManager.variableFood;
        DetermineSickness();

        foreach(FamilyResources f in FindObjectsOfType<FamilyResources>())
        {
            if (!f.isPlayer)
            {
                f.DetermineHunger();
                f.DetermineSickness();
            }
        }

        StartCoroutine(FadeAndSwitchScenes(intermediateSceneName));
    }

    public void LoadMainScene()
    {
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

        sicknessValue += Random.Range(-0.5f, 0.5f);

        int sicknessInt = Mathf.RoundToInt(sicknessValue) - 1;

        if (FamilyResources.playerMedicated) sicknessInt -= 2;

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
