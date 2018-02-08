using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour {

    Light targetLight;

    public float primaryIntensity = 1;
    public float secondaryIntensity = 0.1f;

    public enum FlickerType {Random, Periodical, Pulse}

    public FlickerType flickerType;

    //random variables
    public AnimationCurve weightCurve;
    public float checkPeriod = 0.1f;

    float lastCheck = 0;

    //periodical variables
    public float period = 5;
    public float duration = 1;

    float periodStart = 0;

    private void Start()
    {
        targetLight = GetComponent<Light>();
        targetLight.intensity = primaryIntensity;
    }

    private void Update()
    {
        switch (flickerType)
        {
            case FlickerType.Periodical:
                PeriodUpdate();
                break;

            case FlickerType.Random:
                RandomUpdate();
                break;

            case FlickerType.Pulse:
                Pulse();
                break;
        }
    }

    void RandomUpdate()
    {
        if(Time.time > lastCheck + checkPeriod)
        {
            //print("evaluating");
            if(weightCurve.Evaluate(Random.Range(0f, 1f)) > 0.5f)
            {
                targetLight.intensity = secondaryIntensity;
            }
            else
            {
                targetLight.intensity = primaryIntensity;
            }

            lastCheck = Time.time;
        }
    }

    void PeriodUpdate()
    {
        if (Time.time > periodStart + period)
        {
            periodStart = Time.time;
        }

        if(Time.time > periodStart + duration)
        {
            targetLight.intensity = secondaryIntensity;
        }
        else
        {
            targetLight.intensity = primaryIntensity;
        }
    }

    void Pulse()
    {
        targetLight.intensity = Mathf.Lerp(primaryIntensity, secondaryIntensity, weightCurve.Evaluate((Time.time % period) / period));
    }
}
