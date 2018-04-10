using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation : MonoBehaviour {

    private Animator animator;

    private NPCInteractionBasic interaction;

    Vector3 previousPosition = Vector3.zero;

	void Start () {
        animator = GetComponent<Animator>();
        interaction = transform.parent.GetComponent<NPCInteractionBasic>();
        animator.SetFloat("X Facing", 1);
    }
	
	void Update () {
        Vector3 velocity = (transform.position - previousPosition) / Time.deltaTime;

        animator.SetFloat("Speed", velocity.magnitude);

        previousPosition = transform.position;

        if(interaction.currentState == NPCInteractionBasic.NPCState.WaitingForPlayer)
        {
            animator.SetBool("Beckoning", true);

            Vector3 relative = PlayerMovement.player.transform.position - transform.position;

            animator.SetFloat("X Facing", relative.x / Mathf.Abs(relative.x));
        }
        else
        { 
            animator.SetBool("Beckoning", false);

            if (velocity.magnitude > 0.01f)
            {
                animator.SetFloat("X Facing", velocity.x / Mathf.Abs(velocity.x));
            }
        }
    }
}
