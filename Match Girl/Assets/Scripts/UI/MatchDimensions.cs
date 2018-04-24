using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchDimensions : MonoBehaviour {

    RectTransform rect;
    public RectTransform target;

    public bool matchColor = false;

    private void Start()
    {
        rect = transform as RectTransform;
    }

    private void FixedUpdate()
    {
        rect.sizeDelta = target.sizeDelta;

        if (matchColor)
        {
            GetComponent<Graphic>().color = target.GetComponent<Graphic>().color;
        }
    }
}
