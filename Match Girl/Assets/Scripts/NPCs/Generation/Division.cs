using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Division : ScriptableObject {

    public ModelType modelType;

    public DivisionType divisionType;

    public Part[] parts;

    public Division(ModelType _modelType, DivisionType _divisionType, Part[] _partArray)
    {
        modelType = _modelType;
        divisionType = _divisionType;
        parts = _partArray;
    }

}

public enum DivisionType
{
    hat,
    headAndHands,
    body,
    legs,
    feet
}
