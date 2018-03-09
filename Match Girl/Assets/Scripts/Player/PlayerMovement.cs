using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float baseSpeed = 5;

    private float speed;

    Vector3 movement = Vector3.zero;

    public static GameObject player;

    public LayerMask groundMask;

    private bool frozen = false;

    private Vector3 normal = Vector3.up;

	void Start () {
        player = gameObject;
        speed = baseSpeed;
	}

    private void FixedUpdate()
    {
        if (!frozen)
        {
            movement = Quaternion.FromToRotation(Vector3.up, normal) * Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

            transform.Translate(movement * (speed * Time.deltaTime));
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
        }
        else
        {
            speed = baseSpeed;
        }

        Collider coll = GetComponent<Collider>();

        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hit, Mathf.Infinity, groundMask))
        {
            normal = hit.normal;

            Debug.DrawLine(transform.position, hit.point);
        }
    }
}
