using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EntryDictionary))]
public class AnyDoubleLineSerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }

[CustomPropertyDrawer(typeof(ArchetypeDictionary))]
public class ArchetypeSerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }
