using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour {

    public float fadeInTime;
    public Image fadePanel;
    private Color currentColor = Color.black;
	private bool check = false;

	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad < fadeInTime)
        {
            float alphaChange = Time.deltaTime / fadeInTime;
            currentColor.a -= alphaChange;
            fadePanel.color = currentColor;
        }

		if (check == true) {
			SceneManager.LoadScene ("Start Menu");
		}
		else if (Time.timeSinceLevelLoad > 24)
			check = true;
	

	}
}
