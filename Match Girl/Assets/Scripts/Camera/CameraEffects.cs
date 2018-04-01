using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraEffects : MonoBehaviour {

    public float warmthFXLimit = 25;

    PostProcessingProfile post;

    PlayerTemperature player;

    float shakeTimer = 0;
    float intensity = 0;

    float shakeSmooth = 0.5f;

    Vector3 basePos;

    Vector3 vel;
	
	void Start () {
        post = GetComponent<PostProcessingBehaviour>().profile;
        player = FindObjectOfType<PlayerTemperature>();
        basePos = transform.localPosition;
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

        if(shakeTimer > 0)
        {
            Vector2 shakePos = Random.insideUnitCircle * intensity;
            transform.localPosition = new Vector3(shakePos.x, shakePos.y, basePos.z);
            shakeTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.localPosition.x, basePos.x, ref vel.x, shakeSmooth);
        float posY = Mathf.SmoothDamp(transform.localPosition.y, basePos.y, ref vel.y, shakeSmooth);

        transform.localPosition = new Vector3(posX, posY, basePos.z);
    }

    public void Shake(float _duration, float _intensity)
    {
        intensity = _intensity/100f;
        shakeTimer = _duration;
    }

}
