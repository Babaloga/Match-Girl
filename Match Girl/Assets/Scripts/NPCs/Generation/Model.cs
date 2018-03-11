using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

[CreateAssetMenu]
public class Model : ScriptableObject {

    public ModelType modelType;

    public Part head = new Part(PartType.head);
    public Part torso = new Part(PartType.torso);
    public Part hat = new Part(PartType.hat);
    public Part rightThigh = new Part(PartType.rightThigh);
    public Part leftThigh = new Part(PartType.leftThigh);
    public Part rightShin = new Part(PartType.rightShin);
    public Part leftShin = new Part(PartType.leftShin);
    public Part rightFoot = new Part(PartType.rightFoot);
    public Part leftFoot = new Part(PartType.leftFoot);
    public Part rightUpperArm = new Part(PartType.rightUpperArm);
    public Part leftUpperArm = new Part(PartType.leftUpperArm);
    public Part rightForearm = new Part(PartType.rightForearm);
    public Part leftForearm = new Part(PartType.leftForearm);
    public Part rightHand = new Part(PartType.rightHand);
    public Part leftHand = new Part(PartType.leftHand);
}

public enum PartType
{
    head,
    torso,
    hat,
    rightThigh,
    leftThigh,
    rightShin,
    leftShin,
    rightFoot,
    leftFoot,
    rightUpperArm,
    leftUpperArm,
    rightForearm,
    leftForearm,
    rightHand,
    leftHand
}

public enum ModelType
{
    debug,
    poorMan,
    richMan
}

[System.Serializable]
public class Part
{
    private PartType type;

    public PartType Type
    {
        get
        {
            return type;
        }
    }

    public SpriteMesh spriteMesh;

    public Part(PartType _type)
    {
        type = _type;
    }
}
