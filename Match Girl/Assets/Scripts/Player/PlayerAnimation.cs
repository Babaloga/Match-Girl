using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimation : MonoBehaviour {

    public Animator topAnimator;
    public Animator bottomAnimator;

    Vector3 previousPosition = Vector3.zero;
    PlayerMovement move;

    bool streetScene;

    void Start()
    {
        streetScene = SceneManager.GetActiveScene().name == "Street";

        move = transform.parent.GetComponent<PlayerMovement>();

        topAnimator.SetFloat("X", 1);
        topAnimator.SetFloat("Z", -1);

        bottomAnimator.SetFloat("X", 1);
        bottomAnimator.SetFloat("Z", -1);

        topAnimator.enabled = false;
        topAnimator.enabled = true;
    }

    void Update()
    {
        if (PauseMenu.isPaused == false)
        {
            Vector3 velocity = move.movement;
            topAnimator.speed = 1;
            bottomAnimator.speed = 1;

            topAnimator.SetFloat("Speed", velocity.magnitude);
            bottomAnimator.SetFloat("Speed", velocity.magnitude);

            if(streetScene) topAnimator.SetFloat("Temperature", PlayerStatsManager.Warmth);
            else topAnimator.SetFloat("Temperature", 100f);

            previousPosition = transform.position;

            if (velocity.magnitude > 0.01f)
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
        else
        {
            topAnimator.speed = 0;
            bottomAnimator.speed = 0;
        }

    }

    public void Callout()
    {
        topAnimator.SetTrigger("Matches");
    }
}
