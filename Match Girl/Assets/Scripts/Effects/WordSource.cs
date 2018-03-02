using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordSource : MonoBehaviour {

    public GameObject speakPrefab;
    GameObject speakInstance;
    public bool parentToSource = false;

    public void Speak()
    {
        if (parentToSource)
        {
            speakInstance = Instantiate(speakPrefab, transform);

            speakInstance.transform.localPosition = Vector3.zero;

            //Vector3 a = speakInstance.transform.localScale;
            //Vector3 b = transform.lossyScale;
            //speakInstance.transform.localScale = new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }
        else
        {
            speakInstance = Instantiate(speakPrefab);
            speakInstance.transform.position = transform.position;

        }
    }

    public void Speak(string _content)
    {
        if (parentToSource)
        {
            speakInstance = Instantiate(speakPrefab, transform);

            speakInstance.transform.localPosition = Vector3.zero;
            speakInstance.GetComponent<Text>().text = _content;

            //Vector3 a = speakInstance.transform.localScale;
            //Vector3 b = transform.lossyScale;
            //speakInstance.transform.localScale = new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }
        else
        {
            speakInstance = Instantiate(speakPrefab);
            speakInstance.transform.position = transform.position;

        }
    }
}
