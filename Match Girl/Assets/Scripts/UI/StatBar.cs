using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBar : MonoBehaviour {

    public BarType barType;

	public enum BarType
    {
        Temperature, Sickness, Food
    }

    private void Update()
    {
        float value = 0;

        switch (barType)
        {
            case (BarType.Temperature):
                value = PlayerStatsManager.Warmth;
                break;

            case (BarType.Sickness):
                value = (float)StatusEffects.sicknessLevel * 33f;
                break;

            case (BarType.Food):
                value = (float)StatusEffects.hungerLevel * 33f;
                break;
        }
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, value);
    }
}
