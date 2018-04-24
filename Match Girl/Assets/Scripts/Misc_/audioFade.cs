using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioFade : MonoBehaviour {

	public AudioSource audio;
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad > 16)
			audio.volume -= 0.001f;
	}
}
