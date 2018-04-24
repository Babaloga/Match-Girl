using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSound : MonoBehaviour {

    AudioSource wind1;
    public AudioSource wind2;

    float fullVolume = 0.25f;

    float t;

    private void Start()
    {
        wind1 = GetComponent<AudioSource>();
        wind1.Play();
        wind2.PlayDelayed(wind1.clip.length / 2f);
        t = Time.time;
    }

    private void Update()
    {
        AudioUpdate(wind1);
        AudioUpdate(wind2);
    }

    private void AudioUpdate(AudioSource source)
    {
        float p = source.time / source.clip.length;

        if(p < 0.2f)
        {
            source.volume = Mathf.Lerp(0, fullVolume, p*5);
        }
        else if(p > 0.8f)
        {
            source.volume = Mathf.Lerp(fullVolume, 0, (p - 0.8f) * 5);
        }
    }
}
