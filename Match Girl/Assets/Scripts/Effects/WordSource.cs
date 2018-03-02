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

    public void Speak(float duration, float shimmy, float shimmyFreq, float scaleMultiplier, float speedMultiplier, float alpha)
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

        TextFloat tFloat = speakInstance.GetComponent<TextFloat>();

        tFloat.duration = duration;
        tFloat.shimmyMultiplier = shimmy;
        tFloat.shimmyFreqMultiplier = shimmyFreq;
        tFloat.scaleMultiplier = scaleMultiplier;
        tFloat.speedMultiplier = speedMultiplier;
        tFloat.alphaMultiplier = alpha;
    }
}
