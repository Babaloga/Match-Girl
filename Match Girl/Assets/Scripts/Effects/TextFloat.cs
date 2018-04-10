using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(Rigidbody2D))]
public class TextFloat : MonoBehaviour {
    public float noiseAmount = 0;

    public float duration = 5;

    public float shimmyMultiplier = 1;
    public AnimationCurve shimmyCurve;

    public float shimmyFreqMultiplier = 5;
    public AnimationCurve shimmyFreqCurve;

    public float alphaMultiplier = 1;
    public AnimationCurve alphaCurve;

    public float scaleMultiplier = 2;
    public AnimationCurve scaleCurve;

    public float speedMultiplier = 1;
    public AnimationCurve velocityCurve;

    float startTime;

    Text text; //replace with whatever type of renderer we end up using
    Rigidbody2D rb;

    float phase;

    Vector3 startScale;

    private void Awake()
    {
        text = GetComponent<Text>();
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;

        phase = Random.Range(0, Mathf.PI);
        startScale = transform.localScale;

        if(noiseAmount != 0)
        {
            duration += Random.Range(-noiseAmount, noiseAmount) * duration;
            scaleMultiplier += Random.Range(-noiseAmount, noiseAmount) * scaleMultiplier;
            speedMultiplier += Random.Range(-noiseAmount, noiseAmount) * speedMultiplier;
        }

        float timeElapsed = Time.time - startTime;

        Color currentColor = text.color;

        text.color = new Color(currentColor.r, currentColor.g, currentColor.b, alphaCurve.Evaluate(timeElapsed / duration) * alphaMultiplier);
        rb.velocity = new Vector2(Mathf.Sin((timeElapsed * (shimmyFreqCurve.Evaluate(timeElapsed / duration) * shimmyFreqMultiplier)) + phase) * shimmyCurve.Evaluate(timeElapsed / duration) * shimmyMultiplier, velocityCurve.Evaluate(timeElapsed / duration) * speedMultiplier);
        transform.localScale = startScale * scaleCurve.Evaluate(timeElapsed / duration) * scaleMultiplier;
    }

    private void FixedUpdate()
    {
        float timeElapsed = Time.time - startTime;

        Color currentColor = text.color;

        text.color = new Color(currentColor.r, currentColor.g, currentColor.b, alphaCurve.Evaluate(timeElapsed / duration) * alphaMultiplier);
        rb.velocity = new Vector2(Mathf.Sin((timeElapsed * (shimmyFreqCurve.Evaluate(timeElapsed / duration) * shimmyFreqMultiplier)) + phase) * shimmyCurve.Evaluate(timeElapsed / duration) * shimmyMultiplier, velocityCurve.Evaluate(timeElapsed / duration) * speedMultiplier);
        transform.localScale = startScale * scaleCurve.Evaluate(timeElapsed / duration) * scaleMultiplier;

        if (text.color.a <= 0 || timeElapsed / duration >= 1) Destroy(gameObject);
    }

}
