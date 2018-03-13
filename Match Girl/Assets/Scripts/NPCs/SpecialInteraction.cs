using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialInteraction : MonoBehaviour {

    public float radius = 5f;

    public Dialogue dialogue;

    public Canvas tooltip;

    public bool oneTimeInteraction = false;

	void Start () {
        if(!tooltip) tooltip = GetComponentInChildren<Canvas>();
	}

    private void Update()
    {
        Vector3 relative = PlayerMovement.player.transform.position - transform.position;

        bool near = PlayerCallout.interactions.Contains(this);

        if(relative.magnitude <= radius && !near)
        {
            PlayerCallout.WithinRange(this);
        }
        else if (relative.magnitude > radius && near)
        {
            PlayerCallout.OutofRange(this);
        }
    }
}
