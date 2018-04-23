using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdPoint : MonoBehaviour {

    //This class essentially just marks a transform as a crowd point
    //The actual NPC goal finding is in MoveTo

    public bool allowPoor = true;
    public bool allowRich = true;

    public LayerMask rayMask;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }

    public virtual Vector3 GetPosition()
    {
        bool validPoint = false;
        Vector3 goalPos = Vector3.zero;

        int i = 0;

        while (!validPoint)
        {
            //Break while look if more than 100 iterations occur to prevent freezing.
            if (i > 1000)
            {
                Debug.LogWarning("Couldn't find viable ground under " + transform.name, transform);
                break;
            }

            //Generate random Vector3
            Vector3 randomDirection = Random.insideUnitSphere;
            //Set random Vector3 y axis to a new random value between -0.5 and -1
            randomDirection.y = Random.Range(-1, -0.5f);

            RaycastHit hit;

            //Raycast and check for a walkable surface
            if (Physics.Raycast(transform.position, randomDirection, out hit, Mathf.Infinity, rayMask, QueryTriggerInteraction.Ignore) && hit.collider.gameObject.layer == 12)
            {
                validPoint = true;
                goalPos = hit.point;
            }
            else
            {
                Debug.DrawLine(transform.position, hit.point, Color.red, 1f);
            }

            i++;
        }

        Debug.DrawLine(transform.position, goalPos, Color.green, 2f);

        return goalPos;
    }

}
