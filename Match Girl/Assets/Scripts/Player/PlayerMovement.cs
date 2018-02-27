using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 5;

    Vector3 movement = Vector3.zero;

    WordSource source;
    PlayerCallout callout;

    public float speakCooldown = 3;
    float speakTime = -5;

    public static GameObject player;

    private bool frozen = false;
    private bool muted = false;

	void Start () {
        source = GetComponent<WordSource>();
        callout = FindObjectOfType<PlayerCallout>();
        player = gameObject;
	}
	
	void FixedUpdate () {

        if (!frozen)
        {
            movement = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

            transform.Translate(movement * (speed * Time.fixedDeltaTime));
        }

        if (!muted)
        {
            if (Input.GetKeyDown(KeyCode.Space) && (Time.time - speakTime) > speakCooldown)
            {
                source.Speak();
                callout.Callout();
                speakTime = Time.time;
            }
        }

        if (DialogueReader.reader.showingDialogue)
        {
            frozen = true;
            muted = true;
        }
        else
        {
            frozen = false;
            muted = false;
        }
    }
}
