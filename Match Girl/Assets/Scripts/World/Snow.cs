using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        snowE = snow.emission;
    }

    private void Update()
    {
        chooseWeather();
        if (changed)
        {
            // add environmental changes (snow) here
            if (snowState == SnowState.noSnow) 
            {
                snowE.rateOverTime = 0;
                PlayerTemperature.worldTemperature = 40;
                PlayerTemperature.conductivity = 1;
            }

            else if (snowState == SnowState.lightSnow)
            {
                snowE.rateOverTime = 50;
                PlayerTemperature.worldTemperature = 20;
                PlayerTemperature.conductivity = 3;
            }
            else if (snowState == SnowState.mediumSnow)
            {
                snowE.rateOverTime = 100;
                PlayerTemperature.worldTemperature = -50;
                PlayerTemperature.conductivity = 5;
            }
            else
            {
                snowE.rateOverTime = 200;
                PlayerTemperature.worldTemperature = -100;
                PlayerTemperature.conductivity = 8;
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
