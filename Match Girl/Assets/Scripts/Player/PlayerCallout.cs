using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerCallout : MonoBehaviour {

    SphereCollider cast;

    public float callRadius = 15;
    public float callSpeed = 1;

	void Start () {
        cast = GetComponent<SphereCollider>();
        cast.radius = 0;
        cast.isTrigger = true;
        gameObject.layer = 13;
        transform.localScale = new Vector3(1 / transform.parent.localScale.x, 1 / transform.parent.localScale.y, 1 / transform.parent.localScale.z);
	}
	
	public void Callout()
    {
        StopAllCoroutines();
        StartCoroutine(CalloutRoutine());
    }

    IEnumerator CalloutRoutine()
    {
        while(cast.radius < callRadius)
        {
            cast.radius += callSpeed * Time.deltaTime;
            yield return null;
        }
        cast.radius = 0;
    }
}
