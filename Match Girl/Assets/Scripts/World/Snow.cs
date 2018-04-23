using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Snow : MonoBehaviour {

    //                       0           100      1000      5000
    public enum SnowState { noSnow, lightSnow, mediumSnow, snowStorm};
    public static SnowState snowState;
    //public ParticleSystem snow;
    private bool changed = false;
    private ParticleSystem.EmissionModule snowE;


    ParticleSystem snow;


// Use this for initialization
    void Start () {
        //InvokeRepeating("", 30F, 30F);
        snowState = SnowState.snowStorm;
        changed = true;
        snow = GetComponent<ParticleSystem>();
        print(snow);
    }

    private void Update()
    {
        
        chooseWeather();
        if (changed)
        {
            // add environmental changes (snow) here
            if (snowState == SnowState.noSnow) 
            {
                snowE = snow.emission;
                snowE.rateOverTime = 0;
                
                DayNightCycle.instance.cycleHighTemperature = 40;
            }

            else if (snowState == SnowState.lightSnow)
            {
                snowE = snow.emission;
                snowE.rateOverTime = 50;

                DayNightCycle.instance.cycleHighTemperature = 30;
            }
            else if (snowState == SnowState.mediumSnow)
            {
                snowE = snow.emission;
                snowE.rateOverTime = 100;

                DayNightCycle.instance.cycleHighTemperature = 15;
            }
            else
            {
                snowE = snow.emission;
                snowE.rateOverTime = 200;

                DayNightCycle.instance.cycleHighTemperature = 0;
            }

            changed = false;
        }
    }
    void chooseWeather()
    {
        float change = Random.Range(0, 100);

        if (change < 5)
        {
            float weather = Random.Range(0, 4);
            if (weather < 1)
            {
                snowState = SnowState.noSnow;
            }
            else if (weather < 2 && weather >= 1)
            {
                snowState = SnowState.lightSnow;
            }
            else if (weather < 3 && weather >= 2)
            {
                snowState = SnowState.mediumSnow;
            }
            else
            {
                snowState = SnowState.snowStorm;
            }

            changed = true;
        }
    }

}
