using UnityEngine;

public class AudioReaction : Reaction
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float delay;
    //public float radius;
    //public bool soundOnlyWhenNearby=true;
    //Transform player;

    

    protected override void ImmediateReaction()
    //{
        //player = FindObjectOfType<PlayerNoPointer>().transform;

        //float relativeX = player.position.x- transform.position;
        //if (relativeX <= radius)
        //{

        //}


        //else
        {
            audioSource.clip = audioClip;
            audioSource.PlayDelayed(delay);
        }
    //}
}