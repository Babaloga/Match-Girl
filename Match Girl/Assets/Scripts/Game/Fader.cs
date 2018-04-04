using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Fader : MonoBehaviour {

    public CanvasGroup fader;

    public bool fadeOnAwake = true;

    public float fadeDuration = 3f;

    private bool fadingOut = false;
    private bool fadingIn = false;

    public bool interactableWhenIn = true;
    public bool blockRaysWhenIn = true;

    private void Awake()
    {
        fader = GetComponent<CanvasGroup>();
        if (fadeOnAwake)
        {
            fader.alpha = 1;
            fadingOut = true;
        }
    }

    private void FixedUpdate()
    {
        if (fadingOut && fader.alpha > 0)
        {
            fader.alpha -= (1f / fadeDuration) * Time.fixedDeltaTime;
        }
        else if (fadingIn && fader.alpha < 1)
        {
            fader.alpha += (1f / fadeDuration) * Time.fixedDeltaTime;
        }
    }

    public void FadeIn()
    {
        fadingIn = true;
        fadingOut = false;

        if (interactableWhenIn) fader.interactable = true;
        if (blockRaysWhenIn) fader.blocksRaycasts = true;
    }

    public void FadeOut()
    {
        fadingIn = false;
        fadingOut = true;

        fader.interactable = false;
        fader.blocksRaycasts = false;
    }

}
