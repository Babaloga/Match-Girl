using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WarmthSource : MonoBehaviour {

    public float temperature = 80;

    public Collider warmthArea;

    private void Start()
    {
        warmthArea = GetComponent<Collider>();
        warmthArea.isTrigger = true;
    }
}
