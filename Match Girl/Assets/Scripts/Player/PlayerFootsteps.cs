using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootsteps : MonoBehaviour {

    public AudioClip[] footstepSounds;
    public AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Footstep()
    {
        source.clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
        source.Play();
    }
}
