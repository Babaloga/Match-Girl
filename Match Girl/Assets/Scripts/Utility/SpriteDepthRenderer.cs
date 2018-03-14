using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class SpriteDepthRenderer : MonoBehaviour {

    SpriteRenderer rend;
    SpriteMeshInstance mesh;

    int offset;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        mesh = GetComponent<SpriteMeshInstance>();

        offset = Random.Range(0, 20);
    }

    private void Update()
    {
            if (rend)
                rend.sortingOrder = (int)(transform.position.z * -100);
            else if (mesh)
                mesh.sortingOrder = (int)(transform.position.z * -100);
    }
}
