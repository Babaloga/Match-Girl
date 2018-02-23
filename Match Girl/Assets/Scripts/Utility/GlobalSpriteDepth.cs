using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSpriteDepth : MonoBehaviour {

    SpriteRenderer[] sceneRenderers;

	void Start () {
        sceneRenderers = FindObjectsOfType<SpriteRenderer>() as SpriteRenderer[];

        foreach(SpriteRenderer s in sceneRenderers)
        {
            s.gameObject.AddComponent<SpriteDepthRenderer>();
        }
	}
}
