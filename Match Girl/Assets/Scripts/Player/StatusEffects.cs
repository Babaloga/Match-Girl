using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : MonoBehaviour {

    public PlayerStats baseStats;

    public static EffectLevel sicknessLevel = EffectLevel.None;
    public static EffectLevel coldLevel = EffectLevel.None;
    public static EffectLevel hungerLevel = EffectLevel.None;

    public float callStrengthLossPerLevel = 0.2f;
    public float conductivityBoostPerLevel = 0.2f;

    public float speedLossPerLevel = 1f;

    private void Start()
    {
        baseStats = FindObjectOfType<PlayerStatsManager>().l_stats;
        sicknessLevel = PersistentGameManager.persistentSicknessLevel;
    }

    private void Update()
    {
        if(sicknessLevel != EffectLevel.None)
        {
            int n = (int)sicknessLevel;

            PlayerStatsManager.stats.callStrength = baseStats.callStrength - (callStrengthLossPerLevel * n);
            PlayerStatsManager.stats.conductivity = baseStats.conductivity + (conductivityBoostPerLevel * n);
        }

        if(coldLevel != EffectLevel.None)
        {
            int n = (int)coldLevel;

            PlayerStatsManager.stats.speed = baseStats.speed - (speedLossPerLevel * n);
        }
    }
}

public enum EffectLevel { None, Low, Moderate, High}
