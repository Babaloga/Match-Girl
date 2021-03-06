﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class NPCAnimation : MonoBehaviour {

    private Animator animator;
    private AudioSource source;

    private NPCInteractionBasic interaction;
    private SpecialInteraction special;

    Vector3 previousPosition = Vector3.zero;

    public Vector3 scriptedMovement = Vector3.zero;
    public bool scriptedBeckoning = false;

    public AudioClip[] footsteps;

    bool streetScene;

    public enum NPCType
    {
        Generic, Special, Scripted
    }

    public NPCType thisType = NPCType.Generic;

	void Start () {

        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

        streetScene = SceneManager.GetActiveScene().name == "Street";

        switch (thisType)
        {
            case NPCType.Generic:

                interaction = transform.parent.GetComponent<NPCInteractionBasic>();
                animator.SetFloat("X Facing", 1);

                break;

            case NPCType.Special:

                special = transform.parent.GetComponent<SpecialInteraction>();
                animator.SetBool("Beckoning", scriptedBeckoning);
                animator.SetFloat("X Facing", scriptedMovement.x / Mathf.Abs(scriptedMovement.x));

                break;

            case NPCType.Scripted:

                animator.SetBool("Beckoning", scriptedBeckoning);
                animator.SetFloat("Speed", scriptedMovement.magnitude);
                animator.SetFloat("X Facing", scriptedMovement.x / Mathf.Abs(scriptedMovement.x));

                break;
        }
        

    }
	
	void Update () {

        if (PauseMenu.isPaused == false)
        {
            animator.speed = 1;
            switch (thisType)
            {
                case NPCType.Generic:

                    Vector3 velocity = (transform.position - previousPosition) / Time.deltaTime;

                    animator.SetFloat("Speed", velocity.magnitude);

                    previousPosition = transform.position;

                    if (interaction.currentState == NPCInteractionBasic.NPCState.WaitingForPlayer)
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
                    break;

                case NPCType.Special:

                    animator.SetBool("Beckoning", scriptedBeckoning);

                    break;

                case NPCType.Scripted:

                    animator.SetBool("Beckoning", scriptedBeckoning);
                    animator.SetFloat("Speed", scriptedMovement.magnitude);
                    animator.SetFloat("X Facing", scriptedMovement.x / Mathf.Abs(scriptedMovement.x));

                    break;
            }
        }
        else
        {
            animator.speed = 0;
        }

    }

    public void Footstep()
    {
        source.clip = footsteps[Random.Range(0, footsteps.Length)];
        if (streetScene) source.volume = (1f - ((transform.position - PlayerMovement.player.transform.position).magnitude / 15f)) / 10f;
        else source.volume = 0.5f;
        source.Play();
    }
}
