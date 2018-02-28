using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour {

    public static int matches = 0;
    public static int money = 0;
    public static float hunger = 100;
    private static float _warmth;

    public int startingMatches = 0;
    public int startingMoney = 0;
    public float startingHunger = 100;

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

    void Start () {
        temperatureClass = FindObjectOfType<PlayerTemperature>();

        matches = startingMatches;
        money = startingMoney;
        hunger = startingHunger;
	}

    private void Update()
    {
        _warmth = temperatureClass.temperature;
    }
}
