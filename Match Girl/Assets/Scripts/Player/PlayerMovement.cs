using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 5;

    Vector3 movement = Vector3.zero;

    public static GameObject player;

    private bool frozen = false;

	void Start () {
        player = gameObject;
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
        if (DialogueReader.reader.showingDialogue || Input.GetKey(KeyCode.Space))
        {
            frozen = true;
        }
        else
        {
            frozen = false;
        }
    }
}
