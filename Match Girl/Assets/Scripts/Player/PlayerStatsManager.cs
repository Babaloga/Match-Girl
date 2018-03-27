using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour {

    public static int matches = 0;
    public static int money = 0;
    public static float hunger = 100;
    private static float _warmth;

    public int l_matches = 0;
    public int l_money = 0;
    public float l_hunger = 100;
    public float l_warmth;

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

    void Start () {
        temperatureClass = FindObjectOfType<PlayerTemperature>();

        if (PersistentGameManager.instance.currentDay == 1)
        {
            matches = l_matches;
            money = l_money;
            hunger = l_hunger;
        }
        else
        {
            matches = PersistentGameManager.matches;
            money = PersistentGameManager.money;
            hunger = PersistentGameManager.hunger;
        }
	}

    private void Update()
    {
        _warmth = temperatureClass.temperature;

#if UNITY_EDITOR
        l_matches = matches;
        l_money = money;
        l_hunger = hunger;
        l_warmth = _warmth;
#endif

        if(_warmth < min_warmth)
        {
            GameStateManager.state = GameState.dead;
        }

    }
}
