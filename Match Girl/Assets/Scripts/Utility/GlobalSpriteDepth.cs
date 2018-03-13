using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class GlobalSpriteDepth : MonoBehaviour {

    SpriteRenderer[] sceneRenderers;
    SpriteMeshInstance[] sceneInstances;

	void Start () {
        sceneRenderers = FindObjectsOfType<SpriteRenderer>() as SpriteRenderer[];
        sceneInstances = FindObjectsOfType<SpriteMeshInstance>() as SpriteMeshInstance[];

        foreach(SpriteRenderer s in sceneRenderers)
        {
            if(!s.GetComponent<SpriteDepthRenderer>()) s.gameObject.AddComponent<SpriteDepthRenderer>();
        }

        foreach(SpriteMeshInstance s in sceneInstances)
        {
            if (!s.GetComponent<SpriteDepthRenderer>()) s.gameObject.AddComponent<SpriteDepthRenderer>();
        }
	}
}
