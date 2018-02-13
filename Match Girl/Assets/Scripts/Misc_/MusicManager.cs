using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public AudioClip[] acArray;
    // Use this for initialization

    private AudioSource audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip levelMusic = acArray[scene.buildIndex];

        if (levelMusic)  // if there is music for this level
        {
            audioSource.clip = levelMusic;
            audioSource.volume = PlayerPrefsManager.getMasterVolume();
            audioSource.loop = true;
            audioSource.Play();

        }
    }

    public void changeVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
