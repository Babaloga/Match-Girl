using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

    private void Awake()
    {
        Vector3 relative = Camera.main.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relative);
    }

    void Update () {
        Vector3 relative = Camera.main.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relative);
	}
}
