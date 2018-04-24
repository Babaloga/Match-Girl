using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageFlicker : MonoBehaviour {

    Image target;

    public Color primaryColor = Color.white;
    public Color secondaryColor = Color.black;

    public enum FlickerType { Random, Periodical, Pulse }

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
        target = GetComponent<Image>();
        target.color = primaryColor;
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
        if (Time.time > lastCheck + checkPeriod)
        {
            //print("evaluating");
            if (weightCurve.Evaluate(Random.Range(0f, 1f)) > 0.5f)
            {
                target.color = secondaryColor;
            }
            else
            {
                target.color = primaryColor;
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

        if (Time.time > periodStart + duration)
        {
            target.color = secondaryColor;
        }
        else
        {
            target.color = primaryColor;
        }
    }

    void Pulse()
    {
        target.color = Color.Lerp(primaryColor, secondaryColor, weightCurve.Evaluate((Time.time % period) / period));
    }
}
