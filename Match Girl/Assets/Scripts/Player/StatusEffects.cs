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
        int sick = (int)sicknessLevel;

        PlayerStatsManager.stats.callStrength = baseStats.callStrength - (callStrengthLossPerLevel * sick);
        PlayerStatsManager.stats.conductivity = baseStats.conductivity + (conductivityBoostPerLevel * sick);


        int cold = (int)coldLevel;

        PlayerStatsManager.stats.speed = baseStats.speed - (speedLossPerLevel * cold);


        if (PlayerStatsManager.stats.boots)
        {
            PlayerStatsManager.stats.conductivity = PlayerStatsManager.stats.conductivity * 0.75f;
        }

        if (PlayerStatsManager.stats.warmClothes)
        {
            PlayerStatsManager.stats.conductivity = PlayerStatsManager.stats.conductivity * 0.5f;
        }
    }
}

public enum EffectLevel { None, Low, Moderate, High}
