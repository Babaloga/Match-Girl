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

	void Start () {
        source = GetComponent<WordSource>();
        callout = FindObjectOfType<PlayerCallout>();
	}
	
	void FixedUpdate () {
        movement = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

        transform.Translate(movement * (speed * Time.fixedDeltaTime));

        if (Input.GetKeyDown(KeyCode.Space) && (Time.time - speakTime) > speakCooldown)
        {
            source.Speak();
            callout.Callout();
            speakTime = Time.time;
        }
    }
}
