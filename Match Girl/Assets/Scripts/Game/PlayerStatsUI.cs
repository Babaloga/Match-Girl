using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerStatsUI : MonoBehaviour {

    Text text;

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

                break;

            case StatType.Money:

                text.text = PlayerStatsManager.money + " Pence";

                break;
        }
    }
}
