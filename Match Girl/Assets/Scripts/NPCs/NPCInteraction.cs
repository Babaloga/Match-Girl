using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour {

    Collider coll;
    WordSource source;

	void Start () {
        coll = GetComponent<Collider>();
        source = GetComponent<WordSource>();
	}

    private void OnTriggerEnter(Collider other)
    {
        print("triggered");
        if (other.gameObject.layer == 13)
        {
            source.Speak();
        }
    }
}
