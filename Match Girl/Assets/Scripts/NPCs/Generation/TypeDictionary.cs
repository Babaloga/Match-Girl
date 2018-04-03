using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArchetypeDictionary : SerializableDictionary<NPCType, Archetype> { }

public class TypeDictionary : MonoBehaviour {

    public static TypeDictionary instance;

    public ArchetypeDictionary maleDictionary;
    public ArchetypeDictionary femaleDictionary;

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }

}
