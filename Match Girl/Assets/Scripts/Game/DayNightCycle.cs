using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

    public float cycleDuration = 300;
    public float daylightDuration = 150;
    public float cycleStart = 0;

    public float cycleHighTemperature = 100;
    public float cycleLowTemperature = 0;
    private float temperatureDifference;

    public Transform sunObject;
    public Light sun;

    public AnimationCurve dayTemperatureCurve;
    public AnimationCurve nightTemperatureCurve;

    private static float l_currentTime;

    public static float currentTime
    {
        get
        {
            return l_currentTime;
        }
        protected set
        {
            l_currentTime = value;
        }
    }

    private float cycleStartMarker;

    private void Start()
    {
        cycleStartMarker = Time.time - cycleStart;
        temperatureDifference = cycleHighTemperature - cycleLowTemperature;
    }

    private void Update()
    {
        currentTime = Time.time - cycleStartMarker;

        if (currentTime <= daylightDuration) {
            sun.intensity = 1;
            float dayPercentage = currentTime / daylightDuration;

            sunObject.localRotation = Quaternion.Euler(new Vector3(180f * dayPercentage, sunObject.localRotation.y, sunObject.localRotation.z));

            PlayerTemperature.worldTemperature = cycleLowTemperature + (dayTemperatureCurve.Evaluate(dayPercentage) * temperatureDifference);
        }
        else
        {
            float nightPercentage = (currentTime - daylightDuration) / (cycleDuration - daylightDuration);

            if(nightPercentage <= 0.2f)
            {
                sun.intensity = Mathf.Lerp(1, 0, nightPercentage / 0.2f);
            }
            else if (nightPercentage >= 0.8f)
            {
                sun.intensity = Mathf.Lerp(0, 1, (nightPercentage - 0.8f) / 0.2f);
            }
            else
            {
                sun.intensity = 0;
            }

            sunObject.localRotation = Quaternion.Euler(new Vector3(180f + (180f * nightPercentage), sunObject.localRotation.y, sunObject.localRotation.z));

            PlayerTemperature.worldTemperature = cycleLowTemperature + (nightTemperatureCurve.Evaluate(nightPercentage) * temperatureDifference);
        }

        if(currentTime >= cycleDuration)
        {
            currentTime = 0;
            cycleStartMarker = Time.time;
        }
    }

}
