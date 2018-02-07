using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSource : MonoBehaviour {

    public GameObject speakPrefab;
    GameObject speakInstance;

    public void Speak()
    {
        speakInstance = Instantiate(speakPrefab);
        speakInstance.transform.position = transform.position;
        //Vector3 a = speakInstance.transform.localScale;
        //Vector3 b = transform.lossyScale;
        //speakInstance.transform.localScale = new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }
}
