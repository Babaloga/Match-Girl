using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MeshRenderer))]
public class BuildingCamPass : MonoBehaviour {

    public int defaultLayer = 8;
    public int passLayer = 9;

    public Transform target;
    public Collider camBubble;
    public Collider building;

    public float fadeRate = 1.5f;

    public Material[] opaqueMaterials;
    public Material[] instanceMaterials;

    float time1 = 0;
    float time2 = 0;

    bool behind = false;
    bool bubbleOverlaps = false;

    public MeshRenderer rend;

    private void Start()
    {
        if(!building) building = GetComponent<Collider>();
        if(!rend) rend = GetComponent<MeshRenderer>();
        opaqueMaterials = rend.materials;
        List<Material> startMaterialsTemp = new List<Material>();
        for (int i = 0; i < opaqueMaterials.Length; i++)
        {
            Material m = new Material(opaqueMaterials[i]);

            m.name += "[INSTANCE]";

            StandardShaderUtils.ChangeRenderMode(m, StandardShaderUtils.BlendMode.Fade);

            startMaterialsTemp.Add(m);
            //print(i + " " + string.Join(",", instanceMaterials[i].shaderKeywords));
        }
        instanceMaterials = startMaterialsTemp.ToArray();
    }

    private void Update()
    {
        Collider[] overlapping = Physics.OverlapBox(building.bounds.center, building.bounds.extents);

        bubbleOverlaps = false;

        foreach(Collider o in overlapping)
        {
            if(o == camBubble)
            {
                bubbleOverlaps = true;
            }
        }

        if(target.position.z > building.transform.position.z + building.bounds.extents.z)
        {
            gameObject.layer = passLayer;

            behind = true;
        }
        else
        {
            if (target.position.z < building.transform.position.z - building.bounds.extents.z)
            {
                gameObject.layer = passLayer;
            }
            else
            {
                gameObject.layer = defaultLayer;
            }
            behind = false;
        }

        //print(gameObject.name + " " + behind + " " + bubbleOverlaps);

        if(behind && bubbleOverlaps)
        {
            time1 = Time.time;
            Material[] tempMaterials = rend.materials;
            for (int i = 0; i < opaqueMaterials.Length; i++)
            {
                tempMaterials[i] = instanceMaterials[i];
                tempMaterials[i].color = new Color(tempMaterials[i].color.r, tempMaterials[i].color.g, tempMaterials[i].color.b, Mathf.Lerp(1, 0, (Time.time - time2) / fadeRate));
            }
            rend.materials = tempMaterials;

        }
        else
        {
            time2 = Time.time;
            Material[] tempMaterials = rend.materials;
            for (int i = 0; i < opaqueMaterials.Length; i++)
            {
                tempMaterials[i].color = new Color(tempMaterials[i].color.r, tempMaterials[i].color.g, tempMaterials[i].color.b, Mathf.Lerp(0, 1, (Time.time - time1) / fadeRate));
                if (rend.material.color.a >= 1) tempMaterials[i] = opaqueMaterials[i];
            }
            rend.materials = tempMaterials;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawCube(building.bounds.center, building.bounds.size);
    //}
}

 public static class StandardShaderUtils
{
    public enum BlendMode
    {
        Opaque,
        Cutout,
        Fade,
        Transparent
    }

    public static void ChangeRenderMode(Material standardShaderMaterial, BlendMode blendMode)
    {
        switch (blendMode)
        {
            case BlendMode.Opaque:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                standardShaderMaterial.SetInt("_ZWrite", 1);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = -1;
                break;
            case BlendMode.Cutout:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                standardShaderMaterial.SetInt("_ZWrite", 1);
                standardShaderMaterial.EnableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 2450;
                break;
            case BlendMode.Fade:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                standardShaderMaterial.SetInt("_ZWrite", 0);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.EnableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 3000;
                break;
            case BlendMode.Transparent:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                standardShaderMaterial.SetInt("_ZWrite", 0);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 3000;
                break;
        }

    }
}
