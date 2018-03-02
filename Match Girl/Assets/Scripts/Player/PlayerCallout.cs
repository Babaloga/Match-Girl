using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerCallout : MonoBehaviour {

    SphereCollider cast;
    WordSource source;

    public float callDuration = 1;

    [Range(0, 1)]
    public float powerMin = 0.1f;
    public float timeMax = 5;
    public float throatHealth = 1;

    private bool muted = false;
    private bool down = false;

    public float speakCooldown = 3;
    float speakTime = -5;

    public static Queue<NPCInteraction> npcQueue;

    public AnimationCurve powerCurve;

    int n = 0;

    float spaceDown;

	void Start () {
        npcQueue = new Queue<NPCInteraction>();
        source = GetComponent<WordSource>();
        cast = GetComponent<SphereCollider>();
        cast.radius = 0;
        cast.isTrigger = true;
        cast.enabled = false;
        gameObject.layer = 13;
        transform.localScale = new Vector3(1 / transform.parent.localScale.x, 1 / transform.parent.localScale.y, 1 / transform.parent.localScale.z);
	}

    private void Update()
    {
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

        if (!muted)
        {
            if (Input.GetKeyDown(KeyCode.Space) && (Time.time - speakTime) > speakCooldown)
            {
                spaceDown = Time.time;
                down = true;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (down)
                {
                    speakTime = Time.time;

                    float power = powerCurve.Evaluate(Mathf.Clamp01((Time.time - spaceDown) / timeMax));

                    print(power);

                    Callout(Mathf.Clamp(power , powerMin, 1));
                }
                down = false;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                print(Mathf.Clamp01((Time.time - spaceDown) / timeMax));
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
        Callout(0.1f);
        yield return new WaitForSeconds(1);
        Callout(0.3f);
        yield return new WaitForSeconds(1);
        Callout(0.5f);
        yield return new WaitForSeconds(2);
        Callout(0.7f);
        yield return new WaitForSeconds(2);
        Callout(0.9f);
        yield return new WaitForSeconds(3);
        Callout(1f);
        yield return new WaitForSeconds(3);

        throatHealth = 0.5f;
        print("Throat Health 0.5");
        Callout(0.1f);
        yield return new WaitForSeconds(1);
        Callout(0.3f);
        yield return new WaitForSeconds(1);
        Callout(0.5f);
        yield return new WaitForSeconds(2);
        Callout(0.7f);
        yield return new WaitForSeconds(2);
        Callout(0.9f);
        yield return new WaitForSeconds(3);
        Callout(1f);
        yield return new WaitForSeconds(3);

        throatHealth = 0;
        print("Throat Health 0");
        Callout(0.1f);
        yield return new WaitForSeconds(1);
        Callout(0.3f);
        yield return new WaitForSeconds(1);
        Callout(0.5f);
        yield return new WaitForSeconds(2);
        Callout(0.7f);
        yield return new WaitForSeconds(2);
        Callout(0.9f);
        yield return new WaitForSeconds(3);
        Callout(1f);
        yield return new WaitForSeconds(3);
    }

    public void Callout(float _power)
    {
        StartCoroutine(CalloutRoutine(_power));
        //throatHealth -= _power / 10;
        print(_power);
        source.Speak(
            Mathf.Pow(_power, 2) * 5,
            (-throatHealth + 1) * 10,
            (-throatHealth + 1) * 50,
            (_power) * 5,      
            (-3 * _power) + 4,
            (0.75f * throatHealth) + 0.25f //Mathf.Clamp((throatHealth/100) + 0.25f, 0.01f, 1)
            );
    }

    IEnumerator CalloutRoutine(float _power)
    {
        float radius = _power;

        cast.enabled = true;

        float t = Time.time;
        while ((Time.time - t) <= callDuration)
        {
            float p = (Time.time - t) / callDuration;

            cast.radius = Mathf.Lerp(0, radius, p);
            yield return null;
        }
        cast.radius = 0;
        cast.enabled = false;
    }

    //private void OnDrawGizmos()
    //{
    //    if(Input.GetKey(KeyCode.Space)) Gizmos.DrawSphere(transform.position, Mathf.Clamp(powerCurve.Evaluate((Time.time - spaceDown) / timeMax) * timePowerMultiplier, powerMin, (timePowerMultiplier) - (100/throatHealth) * 5));
    //}
}
