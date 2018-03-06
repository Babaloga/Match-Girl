using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour {

    //                       0           100      1000      10000
    public enum SnowState { noSnow, lightSnow, mediumSnow, snowStorm};
    public SnowState snowState;
    public ParticleSystem snow;
    private bool changed;




    // Use this for initialization
    void Start () {

        changed = false;
        //InvokeRepeating("", 30F, 30F);
        snowState = SnowState.snowStorm;
        
        snow = GetComponent<ParticleSystem>();
        var emission = snow.emission;
        emission.rateOverTime = 10000;
    }

    private void Update()
    {
        if (changed)
        {
            // add environmental changes (snow) here
            if (snowState == SnowState.noSnow) 
            {
                var emission = snow.emission;
                emission.rateOverTime = 0;
            }

            else if (snowState == SnowState.lightSnow)
            {
                var emission = snow.emission;
                emission.rateOverTime = 100;
            }
            else if (snowState == SnowState.mediumSnow)
            {
                var emission = snow.emission;
                emission.rateOverTime = 1000;
            }
            else
            {
                var emission = snow.emission;
                emission.rateOverTime = 10000;
            }
        }
    }

}
