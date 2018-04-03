using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour {

    public static PlayerStats stats;

    public PlayerStats l_stats;

    public float startingWarmth = 50;

    private static float _warmth;

    public float min_warmth = 0;

    public static float Warmth
    {
        get
        {
            return _warmth;
        }
    }

    private static PlayerTemperature temperatureClass;

    public static void SetTemperature(float _temperature)
    {
        temperatureClass.temperature = _temperature;
    }

    void Awake () {
        temperatureClass = FindObjectOfType<PlayerTemperature>();

        if (PersistentGameManager.currentDay == 1)
        {
            stats = l_stats;
        }
        else
        {
            stats = PersistentGameManager.persistentStats;
        }
	}

    private void Update()
    {
        _warmth = temperatureClass.temperature;

#if UNITY_EDITOR
        l_stats = stats;
#endif

        if (_warmth <= min_warmth + 10) StatusEffects.coldLevel = EffectLevel.High;
        else if (_warmth <= min_warmth + 20) StatusEffects.coldLevel = EffectLevel.Moderate;
        else if (_warmth <= min_warmth + 30) StatusEffects.coldLevel = EffectLevel.Low;
        else StatusEffects.coldLevel = EffectLevel.None;

        if (_warmth < min_warmth)
        {
            GameStateManager.state = GameState.dead;
        }

        if (stats.food <= 0)
        {
            GameStateManager.state = GameState.dead;
        }

    }
}
