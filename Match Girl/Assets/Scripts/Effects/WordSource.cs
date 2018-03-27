using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordSource : MonoBehaviour {

    public GameObject speakPrefab;
    GameObject speakInstance;
    public bool parentToSource = false;

    public Vector3 offset = Vector3.zero;

    public Renderer layerSource;

    public void Speak()
    {
        if (parentToSource)
        {
            speakInstance = Instantiate(speakPrefab, transform);

            speakInstance.transform.localPosition = offset;

            //Vector3 a = speakInstance.transform.localScale;
            //Vector3 b = transform.lossyScale;
            //speakInstance.transform.localScale = new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }
        else
        {
            speakInstance = Instantiate(speakPrefab);
            speakInstance.transform.position = transform.position + offset;

        }

        if (layerSource) speakInstance.GetComponent<Canvas>().sortingOrder = layerSource.sortingOrder;
    }

    public void Speak(string _content)
    {
        if (parentToSource)
        {
            speakInstance = Instantiate(speakPrefab, transform);

            speakInstance.transform.localPosition = offset;
            speakInstance.GetComponent<Text>().text = _content;

            //Vector3 a = speakInstance.transform.localScale;
            //Vector3 b = transform.lossyScale;
            //speakInstance.transform.localScale = new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }
        else
        {
            speakInstance = Instantiate(speakPrefab);
            speakInstance.transform.position = transform.position + offset;

        }
        if(layerSource) speakInstance.GetComponent<Canvas>().sortingOrder = layerSource.sortingOrder;
    }

    public void Speak(float duration, float shimmy, float shimmyFreq, float scaleMultiplier, float speedMultiplier, float alpha)
    {
        if (parentToSource)
        {
            speakInstance = Instantiate(speakPrefab, transform);

            speakInstance.transform.localPosition = offset;

            //Vector3 a = speakInstance.transform.localScale;
            //Vector3 b = transform.lossyScale;
            //speakInstance.transform.localScale = new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }
        else
        {
            speakInstance = Instantiate(speakPrefab);
            speakInstance.transform.position = transform.position + offset;

        }

        TextFloat tFloat = speakInstance.GetComponent<TextFloat>();
        if (layerSource) speakInstance.GetComponent<Canvas>().sortingOrder = layerSource.sortingOrder;

        tFloat.duration = duration;
        tFloat.shimmyMultiplier = shimmy;
        tFloat.shimmyFreqMultiplier = shimmyFreq;
        tFloat.scaleMultiplier = scaleMultiplier;
        tFloat.speedMultiplier = speedMultiplier;
        tFloat.alphaMultiplier = alpha;
    }
}
