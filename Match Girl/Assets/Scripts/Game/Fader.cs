﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Fader : MonoBehaviour {

    public CanvasGroup fader;

    public bool fadeOnAwake = true;

    public float fadeDuration = 3f;

    private bool fadingOut = false;
    private bool fadingIn = false;

    private void Awake()
    {
        fader = GetComponent<CanvasGroup>();
        if (fadeOnAwake)
        {
            fader.alpha = 1;
            fadingOut = true;
        }
    }

    private void Update()
    {
        if (fadingOut && fader.alpha > 0)
        {
            fader.alpha -= (1f / fadeDuration) * Time.unscaledDeltaTime;
        }
        else if (fadingIn && fader.alpha < 1)
        {
            fader.alpha += (1f / fadeDuration) * Time.unscaledDeltaTime;
        }
    }

    public void FadeIn()
    {
        fadingIn = true;
        fadingOut = false;

        fader.interactable = true;
    }

    public void FadeOut()
    {
        fadingIn = false;
        fadingOut = true;

        fader.interactable = false;
    }

}