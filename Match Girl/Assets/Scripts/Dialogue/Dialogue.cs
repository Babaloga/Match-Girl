using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntryDictionary : SerializableDictionary<int, Entry> { }

[CreateAssetMenu]
public class Dialogue : ScriptableObject {

    public EntryDictionary entries;

}
