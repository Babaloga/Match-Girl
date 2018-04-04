using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SphereCollider))]
public class PlayerCallout : MonoBehaviour {

    SphereCollider cast;
    WordSource source;

    public float callDuration = 1;

    [Range(0, 1)]
    public float powerMin = 0.1f;
    public float timeMax = 5;
    public float throatHealth = 1;
    public float maxRadius = 25;

    private bool muted = false;
    private bool down = false;

    public float speakCooldown = 3;
    float speakTime = -5;

    public static Queue<NPCInteraction> npcQueue;

    public AnimationCurve powerCurve;
    public AnimationCurve damageCurve;

    public Image chargeBar;

    int n = 0;

    float spaceDown;

    private float power;

    public static List<SpecialInteraction> interactions;

    MeshRenderer meshRenderer;

    public Color startColor = Color.white;
    public Color damageColor = Color.red;

	void Start () {
        npcQueue = new Queue<NPCInteraction>();
        source = GetComponent<WordSource>();
        cast = GetComponent<SphereCollider>();
        cast.radius = 0;
        cast.isTrigger = true;
        cast.enabled = false;
        gameObject.layer = 13;
        transform.localScale = new Vector3(1 / transform.parent.localScale.x, 1 / transform.parent.localScale.y, 1 / transform.parent.localScale.z);
        interactions = new List<SpecialInteraction>();
        meshRenderer = GetComponent<MeshRenderer>();
	}

    private void Update()
    {
        //Checking for nearby special interactions
        SpecialInteraction nearest = null;
        float nearestDistance = Mathf.Infinity;

        foreach(SpecialInteraction spec in interactions)
        {
            Vector3 relative = spec.transform.position - transform.position;
            if (relative.magnitude < nearestDistance)
            {
                nearest = spec;
                nearestDistance = relative.magnitude;
                if(spec.tooltip) spec.tooltip.gameObject.SetActive(true);
            }
            else
            {
                if (spec.tooltip) spec.tooltip.gameObject.SetActive(false);
            }
        }

        //NPC Dialogue Queueing
        if(npcQueue.Count > 0 && !DialogueReader.reader.showingDialogue)
        {
            if (n > 3)
            {
                NPCInteraction currentNpc = npcQueue.Dequeue();
                currentNpc.SpeakToPlayer();
                n = 0;
            }
            else
            {
                n++;
            }
        }
        else
        {
            n = 0;
        }

        //Callout

        throatHealth = PlayerStatsManager.stats.callStrength;

        if (!muted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (nearest != null)
                {
                    print(nearest.dialogue);
                    DialogueReader.reader.StartDialogue(nearest.dialogue);
                }
                else if ((Time.time - speakTime) > speakCooldown) {
                    spaceDown = Time.time;
                    down = true;
                    power = 0;
                }
            }

            

            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (down)
                {
                    speakTime = Time.time;

                    print(power);

                    Callout(Mathf.Clamp(power , powerMin, 1));
                }
                down = false;
            }

            if (Input.GetKey(KeyCode.Space) && down)
            {
                float rawPercent = Mathf.Clamp01((Time.time - spaceDown) / timeMax);

                power = powerCurve.Evaluate(rawPercent * throatHealth);

                if (power > powerMin)
                {
                    RectTransform rectangle = chargeBar.rectTransform;
                    rectangle.sizeDelta = new Vector2(Mathf.Lerp(0, 750, power), Random.Range(10 - (3 * damageCurve.Evaluate(rawPercent)), 10 + (damageCurve.Evaluate(rawPercent))));
                    chargeBar.color = Color.Lerp(startColor, damageColor, damageCurve.Evaluate(rawPercent));
                    chargeBar.color = new Color(chargeBar.color.r, chargeBar.color.g, chargeBar.color.b, Mathf.Lerp(0,1,rawPercent));
                }
            }
            else
            {
                RectTransform rectangle = chargeBar.rectTransform;
                rectangle.sizeDelta = new Vector2(0, 10);
            }
        }

        if (DialogueReader.reader.showingDialogue)
        {
            muted = true;
        }
        else
        {
            muted = false;
        }

        //DEBUG ONLY

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            StartCoroutine(CalloutTest());
        }
    }

    public IEnumerator CalloutTest()
    {
        throatHealth = 1;
        print("Throat Health 1");
        Callout(0.1f * throatHealth);
        yield return new WaitForSeconds(1);
        Callout(0.3f * throatHealth);
        yield return new WaitForSeconds(1);
        Callout(0.5f * throatHealth);
        yield return new WaitForSeconds(2);
        Callout(0.7f * throatHealth);
        yield return new WaitForSeconds(2);
        Callout(0.9f * throatHealth);
        yield return new WaitForSeconds(3);
        Callout(1f * throatHealth);
        yield return new WaitForSeconds(3);

        throatHealth = 0.5f;
        print("Throat Health 0.5");
        Callout(0.1f * throatHealth);
        yield return new WaitForSeconds(1);
        Callout(0.3f * throatHealth);
        yield return new WaitForSeconds(1);
        Callout(0.5f * throatHealth);
        yield return new WaitForSeconds(2);
        Callout(0.7f * throatHealth);
        yield return new WaitForSeconds(2);
        Callout(0.9f * throatHealth);
        yield return new WaitForSeconds(3);
        Callout(1f * throatHealth);
        yield return new WaitForSeconds(3);

        throatHealth = 0;
        print("Throat Health 0");
        Callout(0.1f * throatHealth);
        yield return new WaitForSeconds(1);
        Callout(0.3f * throatHealth);
        yield return new WaitForSeconds(1);
        Callout(0.5f * throatHealth);
        yield return new WaitForSeconds(2);
        Callout(0.7f * throatHealth);
        yield return new WaitForSeconds(2);
        Callout(0.9f * throatHealth);
        yield return new WaitForSeconds(3);
        Callout(1f * throatHealth);
        yield return new WaitForSeconds(3);
    }

    public void Callout(float _power)
    {
        _power = Mathf.Clamp(_power, powerMin, 1);
        Camera.main.GetComponent<CameraEffects>().Shake(0.2f, damageCurve.Evaluate(_power));
        StartCoroutine(CalloutRoutine(_power));
        //throatHealth -= _power / 10;
        print(_power);
        source.Speak(
            (Mathf.Pow(_power, 2) * 3) + 1,
            (-throatHealth + 1) * 10,
            (-throatHealth + 1) * 50,
            (_power) * 5,      
            (-1 * _power) + 2,
            (0.75f * throatHealth) + 0.25f //Mathf.Clamp((throatHealth/100) + 0.25f, 0.01f, 1)
            );
    }

    IEnumerator CalloutRoutine(float _power)
    {
        float radius = _power * maxRadius;

        cast.enabled = true;
        Color fullColor = meshRenderer.material.GetColor("_RimColor");

        float t = Time.time;
        while ((Time.time - t) <= callDuration)
        {
            float p = (Time.time - t) / callDuration;

            float q = Mathf.Lerp(0, radius, p);

            transform.localScale = new Vector3(q/transform.parent.lossyScale.x,q / transform.parent.lossyScale.y, q / transform.parent.lossyScale.z);
            meshRenderer.material.SetColor("_RimColor", Color.Lerp(fullColor, Color.black, p));
            yield return null;
        }
        transform.localScale = Vector3.zero;
        meshRenderer.material.SetColor("_RimColor", fullColor);
        cast.enabled = false;
    }

    public static void WithinRange(SpecialInteraction _spec)
    {
        interactions.Add(_spec);
    }

    public static void OutofRange(SpecialInteraction _spec)
    {
        interactions.Remove(_spec);
    }
}
