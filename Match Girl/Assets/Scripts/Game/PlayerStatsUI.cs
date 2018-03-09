using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerStatsUI : MonoBehaviour {

    Text text;
    int previousValue;

    public enum StatType
    {
        Money,
        Matches,
    }

    public StatType thisElementType;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        switch (thisElementType)
        {
            case StatType.Matches:

                text.text = PlayerStatsManager.matches + " Matches";

                if(previousValue != PlayerStatsManager.matches)
                {
                    GetComponent<WordSource>().Speak((PlayerStatsManager.matches - previousValue).ToString("+0;-#"));
                }

                previousValue = PlayerStatsManager.matches;
                break;

            case StatType.Money:

                text.text = Currency.FormatPounds(PlayerStatsManager.money);

                if (previousValue != PlayerStatsManager.money)
                {
                    GetComponent<WordSource>().Speak((PlayerStatsManager.money - previousValue).ToString("+0;-#"));
                }

                previousValue = PlayerStatsManager.money;
                break;
        }
    }
}
