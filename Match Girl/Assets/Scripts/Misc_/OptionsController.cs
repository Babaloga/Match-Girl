
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {


    public Slider volumeSlider;
    public Slider difficultySlider;

    public LevelManager levelManager;
    private MusicManager musicManager;

	// Use this for initialization
	void Start () {

        musicManager = FindObjectOfType<MusicManager>();
        volumeSlider.value = PlayerPrefsManager.getMasterVolume();
	}
	
    public void saveAndExit()
    {
        PlayerPrefsManager.setMasterVolume(volumeSlider.value);
        PlayerPrefsManager.setDifficulty(difficultySlider.value);

        levelManager.LoadLevel("Start Menu");
    }

    void Update()
    {
        if (musicManager != null)
        {
            musicManager.changeVolume(volumeSlider.value);
        }
    }

    public void setDefault()
    {
        volumeSlider.value = 0.5f;
        difficultySlider.value = 1f;
    }
}
