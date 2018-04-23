using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

    public float cycleDuration = 300;
    public float daylightDuration = 150;
    public float cycleStart = 0;

    public float cycleHighTemperature = 10;
    public float cycleLowTemperature = 0;
    private float temperatureDifference;

    public Transform sunObject;
    public Light sun;

    public AnimationCurve dayTemperatureCurve;
    public AnimationCurve nightTemperatureCurve;

    public static float currentTime;
    private float tempTime = 0;

    private float cycleStartMarker;

    public bool endDayAutomatically = false;
    public float endDayTime = 140f;

    public static DayNightCycle instance;

    bool ending = false;
    public static bool isNight = false;
    

    private void Start()
    {
        cycleStartMarker = Time.time - cycleStart;
        temperatureDifference = cycleHighTemperature - cycleLowTemperature;

        instance = this;
    }

    public static void SetTime(float _time)
    {
        instance.cycleStartMarker = Time.time - _time;
    }

    private void Update()
    {
        if (PauseMenu.isPaused == false)
        {
            currentTime = Time.time - cycleStartMarker - tempTime;

            if (currentTime <= daylightDuration)
            {
                sun.intensity = 1;
                float dayPercentage = currentTime / daylightDuration;

                sunObject.localRotation = Quaternion.Euler(new Vector3(180f * dayPercentage, sunObject.localRotation.y, sunObject.localRotation.z));

                PlayerTemperature.worldTemperature = cycleLowTemperature + (dayTemperatureCurve.Evaluate(dayPercentage) * temperatureDifference);

                isNight = false;
            }
            else
            {
                float nightPercentage = (currentTime - daylightDuration) / (cycleDuration - daylightDuration);

                if (nightPercentage <= 0.2f)
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

                isNight = true;
            }

            if (endDayAutomatically && currentTime >= endDayTime && !ending)
            {
                ending = true;
                StartCoroutine(FadeOutProcess());
            }

            if (currentTime >= cycleDuration)
            {
                currentTime = 0;
                cycleStartMarker = Time.time;
            }
        }
        else
        {
            tempTime += Time.deltaTime;
        }
    }

    public Fader gettingLate;
    public Fader goHome;

    IEnumerator FadeOutProcess()
    {
       
        gettingLate.FadeIn();

        yield return new WaitForSeconds(1);

        goHome.FadeIn();

        yield return new WaitForSeconds(3);

        PersistentGameManager.instance.LoadEndScene();
    }

}
