using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraEffects : MonoBehaviour {

    public float warmthFXLimit = 25;

    PostProcessingProfile post;

    PlayerTemperature player;
	
	void Start () {
        post = GetComponent<PostProcessingBehaviour>().profile;
        player = FindObjectOfType<PlayerTemperature>();
	}
	

	void Update () {

        float warmthToOne = player.temperature / warmthFXLimit;

        VignetteModel.Settings vSet = post.vignette.settings;

        vSet.center = GetComponent<Camera>().WorldToViewportPoint(player.transform.position);
        vSet.intensity = Mathf.Lerp(0.75f, 0, warmthToOne);

        post.vignette.settings = vSet;

        ColorGradingModel.Settings cSet = post.colorGrading.settings;

        cSet.basic.temperature = Mathf.Lerp(-15, 0, warmthToOne);
        cSet.basic.saturation = Mathf.Lerp(0.1f, 1, warmthToOne);

        post.colorGrading.settings = cSet;
    }
}
