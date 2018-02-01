using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(Rigidbody2D))]
public class TextFloat : MonoBehaviour {
    public float duration = 5;
    public float shimmy = 1;
    public float shimmyFreq = 5;
    public float fullAlpha = 1;
    public float scaleMultiplier = 2;

    public AnimationCurve velocityCurve;

    float startTime;

    Text text; //replace with whatever type of renderer we end up using
    Rigidbody2D rb;

    float random;

    Vector3 startScale;

    private void Start()
    {
        text = GetComponent<Text>();
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;

        random = Random.Range(-10, 10);
        startScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        float timeElapsed = Time.time - startTime;

        Color currentColor = text.color;

        text.color = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(fullAlpha, 0, timeElapsed / duration));
        rb.velocity = new Vector2(Mathf.Sin((timeElapsed * 3) + random) * shimmy, velocityCurve.Evaluate(timeElapsed / duration));
        transform.localScale = startScale * Mathf.Lerp(1, scaleMultiplier, timeElapsed / duration);

        if (text.color.a <= 0) Destroy(gameObject);
    }

}
