using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    public Animator topAnimator;
    public Animator bottomAnimator;

    Vector3 previousPosition = Vector3.zero;
    PlayerMovement move;

    void Start()
    {
        move = transform.parent.GetComponent<PlayerMovement>();

        topAnimator.SetFloat("X", 1);
        topAnimator.SetFloat("Z", -1);

        bottomAnimator.SetFloat("X", 1);
        bottomAnimator.SetFloat("Z", -1);
    }

    void Update()
    {
        Vector3 velocity = move.movement;

        topAnimator.SetFloat("Speed", velocity.magnitude);
        bottomAnimator.SetFloat("Speed", velocity.magnitude);

        previousPosition = transform.position;

        if(velocity.magnitude > 0.01f)
        {
            if (Mathf.Abs(velocity.x) > 0.01f)
            {
                topAnimator.SetFloat("X", velocity.x / Mathf.Abs(velocity.x));
                bottomAnimator.SetFloat("X", velocity.x / Mathf.Abs(velocity.x));
            }
            else
            {
                topAnimator.SetFloat("X", 0);
                bottomAnimator.SetFloat("X", 0);
            }

            if (Mathf.Abs(velocity.z) > 0.01f)
            {
                topAnimator.SetFloat("Z", velocity.z / Mathf.Abs(velocity.z));
                bottomAnimator.SetFloat("Z", velocity.z / Mathf.Abs(velocity.z));
            }
            else
            {
                topAnimator.SetFloat("Z", 0);
                bottomAnimator.SetFloat("Z", 0);
            }
        }

    }
}
