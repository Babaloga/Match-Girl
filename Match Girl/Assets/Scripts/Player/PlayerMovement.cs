using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float baseSpeed = 5;

    private float speed;

    Vector3 movement = Vector3.zero;

    public static GameObject player;

    private bool frozen = false;

	void Start () {
        player = gameObject;
        speed = baseSpeed;
	}
	
	void FixedUpdate () {

        if (!frozen)
        {
            movement = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

            transform.Translate(movement * (speed * Time.fixedDeltaTime));
        }
    }

    private void Update()
    {
        if (DialogueReader.reader.showingDialogue)
        {
            frozen = true;
        }
        else
        {
            frozen = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            speed = (1f / 2f) * baseSpeed;
            print("Speed: "+speed);
        }
        else
        {
            speed = baseSpeed;
        }
    }
}
