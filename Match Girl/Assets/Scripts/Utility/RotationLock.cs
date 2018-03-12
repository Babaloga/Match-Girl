using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLock : MonoBehaviour {

	void Update () {
        transform.localRotation = Quaternion.Euler(new Vector3(0, -transform.parent.rotation.eulerAngles.y, 0));
	}
}
