using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class WordFloat : MonoBehaviour {

    public float duration = 5;
    public float shimmy = 1;
    public float shimmyFreq = 5;
    public float fullAlpha = 1;
    public float scaleMultiplier = 2;

    public AnimationCurve velocityCurve;

    float startTime;

    SpriteRenderer rend; //replace with whatever type of renderer we end up using
    Rigidbody2D rb;

    float random;

    Vector3 startScale;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;

        random = Random.Range(-10, 10);
        startScale = transform.localScale;
    }

    private void Update()
    {
        float timeElapsed = Time.time - startTime;

        Color currentColor = rend.color;

        rend.color = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(fullAlpha, 0, timeElapsed / duration));
        rb.velocity = new Vector2(Mathf.Sin((timeElapsed * 3) + random), velocityCurve.Evaluate(timeElapsed / duration));
        transform.localScale = startScale * Mathf.Lerp(1, scaleMultiplier, timeElapsed / duration);

        if (rend.color.a <= 0) Destroy(gameObject);
    }

}
