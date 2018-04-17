using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class SpriteDepthRenderer : MonoBehaviour {

    public bool advanced = false;

    SpriteRenderer rend;
    SpriteMeshInstance mesh;
    Canvas canvas;

    int offset;

    private enum RenderType
    {
        SpriteRenderer, SpriteMesh, Canvas
    }

    RenderType renderType;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        mesh = GetComponent<SpriteMeshInstance>();
        canvas = GetComponent<Canvas>();

        if (rend)
        {
            renderType = RenderType.SpriteRenderer;
        }
        else if (mesh)
        {
            renderType = RenderType.SpriteMesh;
        }
        else if (canvas)
        {
            renderType = RenderType.Canvas;
        }

        offset = Random.Range(0, 20);
    }

    private void Update()
    {
        if (advanced)
        {
            Vector3 relative = transform.position - Camera.main.transform.position;

            switch (renderType)
            {
                case RenderType.SpriteRenderer:
                    rend.sortingOrder = (int)(relative.magnitude * -100);
                    break;

                case RenderType.SpriteMesh:
                    mesh.sortingOrder = (int)(relative.magnitude * -100);
                    break;

                case RenderType.Canvas:
                    canvas.sortingOrder = (int)(relative.magnitude * -100);
                    break;
            }

            return;
        }

        switch (renderType)
        {
            case RenderType.SpriteRenderer:
                rend.sortingOrder = (int)(transform.position.z * -100);
                break;

            case RenderType.SpriteMesh:
                mesh.sortingOrder = (int)(transform.position.z * -100);
                break;

            case RenderType.Canvas:
                canvas.sortingOrder = (int)(transform.position.z * -100);
                break;
        }              
    }
}
