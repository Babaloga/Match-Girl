﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerStatsUI : MonoBehaviour {

    Text text;
    int previousValue;

    public AudioClip[] clips;

    public enum StatType
    {
        Money,
        Matches,
        Logs,
        Tonics,
        Bandages
    }

    public enum Scene
    {
        Street,
        End
    }

    public StatType thisElementType;

    public Scene thisScene = Scene.Street;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        switch (thisElementType)
        {
            case StatType.Matches:

                if (thisScene == Scene.Street)
                {
                    text.text = PlayerStatsManager.stats.matches.ToString();

                    if (previousValue != PlayerStatsManager.stats.matches)
                    {
                        GetComponent<WordSource>().Speak((PlayerStatsManager.stats.matches - previousValue).ToString("+0;-#"));
                    }

                    previousValue = PlayerStatsManager.stats.matches;
                }
                else
                {
                    text.text = PersistentGameManager.persistentStats.matches + " Matches";
                }

                break;

            case StatType.Money:

                if (thisScene == Scene.Street)
                {
                    text.text = Currency.FormatPounds(PlayerStatsManager.stats.money);

                    if (previousValue != PlayerStatsManager.stats.money)
                    {
                        GetComponent<WordSource>().Speak((PlayerStatsManager.stats.money - previousValue).ToString("+0;-#"));

                        GetComponent<AudioSource>().clip = clips[Random.Range(0, clips.Length)];
                        GetComponent<AudioSource>().Play();
                    }

                    previousValue = PlayerStatsManager.stats.money;

                }
                else
                {
                    text.text = "Money: " + Currency.FormatPounds(PersistentGameManager.persistentStats.money);
                }

                break;

            case StatType.Logs:

                break;

            case StatType.Tonics:

                text.text = PersistentGameManager.persistentStats.tonics.ToString();

                break;

            case StatType.Bandages:

                text.text = PersistentGameManager.persistentStats.bandages.ToString();

                break;
        }
    }
}
