using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTemperature : MonoBehaviour {

    [Range(0, 100)]
    public float temperature = 75;
    public float conductivity = 1;
    [ShowOnly] public float heatflow;

    [ShowOnly] public float outsideTemp;

    List<WarmthSource> heatZones;

    public static float worldTemperature;

    private void Start()
    {
        heatZones = new List<WarmthSource>();
    }

    private void Update()
    {
        outsideTemp = worldTemperature;

        //heatZones is populated and de-populated in OnTriggerEnter and OnTriggerExit
        foreach(WarmthSource w in heatZones)
        {
            //Distance of player from heat source as a fraction of the total trigger radius
            float distance = (transform.position - w.warmthArea.bounds.center).magnitude / w.warmthArea.bounds.extents.x;

            //Heat from this source = (source temperature) * e ^ -5(distance)
            outsideTemp += (w.temperature * Mathf.Exp(-5f * distance));
        }

        //Flow of heat to or from player
        heatflow = ((conductivity/100) * (outsideTemp - temperature)) * Time.deltaTime;

        //Modifying temperature
        temperature += heatflow;

        //Debug: setting sprite color based on temperature
        GetComponent<SpriteRenderer>().color = new Color(temperature / 100, 0, 1 - (temperature / 100));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 11)
        {
            heatZones.Add(other.GetComponent<WarmthSource>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11 && heatZones.Contains(other.GetComponent<WarmthSource>()))
        {
            heatZones.Remove(other.GetComponent<WarmthSource>());
        }
    }
}
