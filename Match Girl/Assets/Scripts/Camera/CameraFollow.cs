using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public SphereCollider coll;

    public GameObject camHolder;
    public float camHolderSpeed = 5;

    public AnimationCurve speedCurve;

    float holderSpeedActual;

    Vector3 holderRelative;

    Vector3 relativePos;
    Vector3 destination;

    bool triggering = false;
    float triggerDirection = 0;
    float buildingX = 0;
    float collisionPointX = 0;

	void Start () {
        relativePos = transform.position - target.position;
        coll = GetComponent<SphereCollider>();
	}
	
	void FixedUpdate () {
        destination = target.position + relativePos;

        Vector3 toDest = destination - transform.position;

        if (triggering)
        {
            print("triggering");
            if (toDest.x == 0 || triggerDirection / toDest.x > 0)
            {
                //destination.x = buildingX - ((triggerDirection / Mathf.Abs(triggerDirection)) * coll.bounds.extents.x);
                print(collisionPointX);

                destination.x = buildingX - collisionPointX;
                toDest = destination - transform.position;
            }
        }

        holderRelative = transform.position - camHolder.transform.position;

        holderSpeedActual = (camHolderSpeed/10) * holderRelative.magnitude;

        if (holderRelative.magnitude > holderSpeedActual)
        {
            holderRelative = holderRelative.normalized * holderSpeedActual;
        }

        Debug.DrawLine(camHolder.transform.position, transform.position);

        camHolder.transform.Translate(holderRelative);

        transform.position = destination;
	}

    //public void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.layer == 8)
    //    {
    //        triggering = true;
    //        triggerDirection = other.transform.position.x - transform.position.x;
    //        buildingX = other.ClosestPointOnBounds(transform.position).x;
    //    }
    //}

    public void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.layer == 8 || collisionInfo.gameObject.layer == 9)
        {
            triggering = false;
        }
    }

    public void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.layer == 8 && triggering == false)
        {
            triggering = true;
            triggerDirection = collisionInfo.transform.position.x - transform.position.x;
            buildingX = collisionInfo.collider.ClosestPointOnBounds(transform.position).x;
        }
        if (collisionInfo.gameObject.layer == 9 && triggering == true)
        {
            triggering = false;
        }

        collisionPointX = 0;

        foreach (ContactPoint c in collisionInfo.contacts)
        {
            float x = c.point.x - transform.position.x;
            if (Mathf.Abs(x) > Mathf.Abs(collisionPointX))
                collisionPointX = x;
        }
    }
}
