using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fader))]
public class DeathUI : MonoBehaviour {

    public static DeathUI instance;
    public AudioClip deathMusic;

    Fader fader;

    public static DeathCause cause;

    private void Start()
    {
        instance = this;
        fader = GetComponent<Fader>();
    }

    public static void ShowDeathUI()
    {
        switch (cause)
        {
            case DeathCause.cold:

                break;

            case DeathCause.hunger:

                break;
        }

        Camera.main.GetComponent<AudioSource>().clip = instance.deathMusic;
        Camera.main.GetComponent<AudioSource>().loop = false;
        if(!Camera.main.GetComponent<AudioSource>().isPlaying) Camera.main.GetComponent<AudioSource>().Play();

        instance.fader.FadeIn();
    }

}

public enum DeathCause
{
    cold,
    hunger
}
