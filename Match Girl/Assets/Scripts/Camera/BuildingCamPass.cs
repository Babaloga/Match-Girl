using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MeshRenderer))]
public class BuildingCamPass : MonoBehaviour {

    public int defaultLayer = 8;
    public int passLayer = 9;

    public Transform target;
    public Collider camBubble;
    Collider building;

    public float fadeRate = 1.5f;

    public Material opaqueMaterial;
    Material instanceMaterial;

    float time1 = 0;
    float time2 = 0;

    bool behind = false;
    bool bubbleOverlaps = false;

    MeshRenderer rend;

    private void Start()
    {
        building = GetComponent<Collider>();
        rend = GetComponent<MeshRenderer>();
        instanceMaterial = new Material(rend.material);
        rend.material = instanceMaterial;
    }

    private void Update()
    {
        Collider[] overlapping = Physics.OverlapBox(building.bounds.center, building.bounds.extents);

        bubbleOverlaps = false;

        foreach(Collider o in overlapping)
        {
            if(o == camBubble)
            {
                bubbleOverlaps = true;
            }
        }

        if(target.position.z > building.transform.position.z + building.bounds.extents.z)
        {
            gameObject.layer = passLayer;

            behind = true;
        }
        else
        {
            if (target.position.z < building.transform.position.z - building.bounds.extents.z)
            {
                gameObject.layer = passLayer;
            }
            else
            {
                gameObject.layer = defaultLayer;
            }
            behind = false;
        }

        if(behind && bubbleOverlaps)
        {
            time1 = Time.time;
            rend.material = instanceMaterial;
            rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, Mathf.Lerp(1, 0, (Time.time - time2) / fadeRate));
        }
        else
        {
            time2 = Time.time;
            rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, Mathf.Lerp(0, 1, (Time.time - time1) / fadeRate));
            if(rend.material.color.a >= 1) rend.material = opaqueMaterial;
        }
    }
}
