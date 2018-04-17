using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float baseSpeed;
    private float speed;

    public Vector3 movement = Vector3.zero;

    public static GameObject player;

    public LayerMask groundMask;

    private bool frozen = false;

    private Vector3 normal = Vector3.up;

    void Awake()
    {
        player = gameObject;
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

        baseSpeed = PlayerStatsManager.stats.speed;

        if (Input.GetKey(KeyCode.Space))
        {
            speed = (0.5f) * baseSpeed;
        }
        else
        {
            speed = baseSpeed;
        }

        //Collider coll = GetComponent<Collider>();

        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hit, Mathf.Infinity, groundMask))
        {
            normal = hit.normal;

            Debug.DrawLine(transform.position, hit.point);
        }
    }
}