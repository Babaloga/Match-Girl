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
        thisRect.position = target.position + (Vector3)(target.rect.center - new Vector2(0, (target.rect.height / 2) * target.lossyScale.y)) 
            + new Vector3((distance.x), (distance.y), 0);
    }
}
