using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureUI : MonoBehaviour {

    public CanvasGroup frostOne;
    public CanvasGroup frostTwo;
    public CanvasGroup frostThree;

    public AnimationCurve frostOneCurve;
    public AnimationCurve frostTwoCurve;
    public AnimationCurve frostThreeCurve;

	void Update () {
        float t = PlayerStatsManager.Warmth;

        frostOne.alpha = frostOneCurve.Evaluate(t/100f);
        frostTwo.alpha = frostTwoCurve.Evaluate(t/100f);
        frostThree.alpha = frostThreeCurve.Evaluate(t/100f);
    }
}
