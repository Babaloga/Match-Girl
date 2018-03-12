using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation : MonoBehaviour {

    private Animator animator;

    Vector3 previousPosition = Vector3.zero;

	void Start () {
        animator = GetComponent<Animator>();
        animator.SetFloat("X Facing", 1);
    }
	
	void Update () {
        Vector3 velocity = (transform.position - previousPosition) / Time.deltaTime;

        if(velocity.magnitude > 0.01f)
            animator.SetFloat("X Facing", velocity.normalized.x);

        animator.SetFloat("Speed", velocity.magnitude);

        previousPosition = transform.position;
	}
}
