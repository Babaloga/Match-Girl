using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativePositioner : MonoBehaviour {

    public Vector2 distance = Vector2.zero;

    public RectTransform target;

    RectTransform thisRect;

    private void Start()
    {
        thisRect = transform as RectTransform;
    }

    private void Update()
    {
        float heightRatio = Screen.height / 450f;

        Debug.DrawLine(target.position, target.position - new Vector3(0, (target.rect.height) * heightRatio, 0), Color.red);
        Debug.DrawLine(target.position - new Vector3(0, (target.rect.height) * heightRatio, 0), target.position - new Vector3(0, (target.rect.height) * heightRatio, 0) - (Vector3)distance * heightRatio, Color.green);

        thisRect.position = target.position - new Vector3(0, (target.rect.height) * heightRatio,0) 
            + /*new Vector3(distance.x * (Screen.width/800), distance.y * (Screen.height/450), 0)*/ (Vector3)distance * heightRatio;
    }
}
