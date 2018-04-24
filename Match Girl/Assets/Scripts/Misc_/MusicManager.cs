using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public AudioClip[] acArray;
    // Use this for initialization

    //public static MusicManager intance;
    public static bool check = false;
    public AudioSource audioSource;
    //private object instance;

    public static MusicManager Instance
    {

        get
        {
            if (Instance == null)
            {
                Instance = new MusicManager();
            }
            return Instance;
        }
        set { }
    }

    private void Awake()
    {
        if (check == false)
        {
            DontDestroyOnLoad(gameObject);
            check = true;
        }
        
    }
    void Start () {

	}



    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        

        AudioClip levelMusic = acArray[scene.buildIndex];
        

        if (levelMusic && check == false)  // if there is music for this level
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
