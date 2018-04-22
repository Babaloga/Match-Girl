
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour {


    public Slider volumeSlider;
    //public Slider difficultySlider;
    public GameObject panel;

    public LevelManager levelManager;
    public MusicManager musicManager;
    
	// Use this for initialization
	void Start () {

        musicManager = FindObjectOfType<MusicManager>();
        volumeSlider.value = PlayerPrefsManager.getMasterVolume();
	}
	
    public void saveAndExit()
    {
        PlayerPrefsManager.setMasterVolume(volumeSlider.value);
        //PlayerPrefsManager.setDifficulty(difficultySlider.value);

        SceneManager.LoadScene("Start Menu");
    }

    void Update()
    {
        if (musicManager != null)
        {
            musicManager.changeVolume(volumeSlider.value);
        }
    }

    public void OpenOptions()
    {
        panel.SetActive(true);
    }
 

    public void setDefault()
    {
        volumeSlider.value = 0.5f;
        //difficultySlider.value = 1f;
    }
}
