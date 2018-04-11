using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndTile : MonoBehaviour {

    public float distance = 10f;

    Vector3 startPos;

    public Vector3 movement = Vector3.zero;

    bool spawnedNext = false;

    public GameObject partner;

    void Awake () {
        startPos = transform.position;
	}
	
	void FixedUpdate () {
        transform.position += movement * Time.fixedDeltaTime;

        if((transform.position - startPos).magnitude >= distance && !partner)
        {
            partner = Instantiate(gameObject, startPos, Quaternion.Euler(Vector3.zero));
            partner.GetComponent<MoveAndTile>().partner = gameObject;
        }

        if ((transform.position - startPos).magnitude >= 2f * distance)
        {
            transform.position = startPos;
        }
    }
}
