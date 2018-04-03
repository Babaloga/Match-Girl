using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdPoint : MonoBehaviour {

    //This class essentially just marks a transform as a crowd point
    //The actual NPC goal finding is in MoveTo

    public bool allowPoor = true;
    public bool allowRich = true;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }

}
