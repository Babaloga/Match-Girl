using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : CrowdPoint {

    public Transform[] outPoints;

    private void Start()
    {
        outPoints = new Transform[transform.childCount];

        for (int i = 0; i < outPoints.Length; i++)
        {
            outPoints[i] = transform.GetChild(i);
        }
    }

    public override Vector3 GetPosition()
    {
        return outPoints[Random.Range(0, outPoints.Length)].position;
    }
}
