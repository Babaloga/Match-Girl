using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTemperature : MonoBehaviour {

    [Range(0, 100)]
    public float temperature = 75;
    public float conductivity = 1;
    public float baselineTemp = 0;
    [ShowOnly] public float heatflow;

    [ShowOnly] public float outsideTemp;

    List<WarmthSource> heatZones;

    private void Start()
    {
        heatZones = new List<WarmthSource>();
    }

    private void Update()
    {
        outsideTemp = baselineTemp;

        foreach(WarmthSource w in heatZones)
        {
            float distance = (transform.position - w.warmthArea.bounds.center).magnitude / w.warmthArea.bounds.extents.x;
            print(distance);
            outsideTemp += (w.temperature * Mathf.Exp(-5f * distance));
        }

        heatflow = conductivity * (outsideTemp - temperature);

        //print(gameObject.name + " " + heatflow);

        //GetComponent<SpriteRenderer>().color = new Color(((heatflow + (100 * conductivity))/ (100 * conductivity)) - 0.5f, 0, 1.5f - ((heatflow + (100 * conductivity)) / (100 * conductivity)));

        temperature += heatflow;

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
