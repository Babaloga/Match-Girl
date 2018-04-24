using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Fader))]
public class DeathUI : MonoBehaviour {

    public static DeathUI instance;
    public AudioClip deathMusic;
    public Text deathText;

    Fader fader;

    public static DeathCause cause;

    private void Start()
    {
        instance = this;
        fader = GetComponent<Fader>();
    }

    public static void ShowDeathUI()
    {
        if (PlayerStatsManager.Warmth < FindObjectOfType<PlayerStatsManager>().min_warmth) {

            instance.deathText.text = "You have frozen to death";

        }
        else if(PlayerStatsManager.stats.food <= 0){
            instance.deathText.text = "You have starved to death";
        }
        else
        {
            instance.deathText.text = "You have died";
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
