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
    public Fader sceneFader;

    public bool debugMode = false;

    public static EffectLevel persistentSicknessLevel;

    private void Start()
    {
        instance = this;
        persistentSicknessLevel = EffectLevel.None;
		persistentStats.boots = false;
        StartCoroutine(LoadSceneSetActive(mainSceneName));
    }

    public void LoadEndScene()
    {
        time = DayNightCycle.currentTime;
        persistentStats = PlayerStatsManager.stats;
        persistentStats.food -= dayHungerPenalty;
        StartCoroutine(FadeAndSwitchScenes(endScreenName));
    }

    public void LoadMainScene()
    {
        currentDay++;
        persistentStats.food = ResourceManager.variableFood;
        StartCoroutine(FadeAndSwitchScenes(mainSceneName));
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
