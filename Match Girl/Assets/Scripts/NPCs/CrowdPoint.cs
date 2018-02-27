using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdPoint : MonoBehaviour {

    //This class essentially just marks a transform as a crowd point
    //The actual NPC goal finding is in MoveTo

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }

}
