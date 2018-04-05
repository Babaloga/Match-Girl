using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialInteraction : MonoBehaviour {

    public string interactionName;

    public float radius = 5f;

    public Dialogue[] dialogues;

    private int i = 0;

    public Canvas tooltip;

    Text tooltipText;

    public bool oneTimeInteraction = false;

	void Start () {
        if(!tooltip) tooltip = GetComponentInChildren<Canvas>();

        tooltipText = GetComponentInChildren<Text>();
        tooltipText.text = interactionName;
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
            tooltip.gameObject.SetActive(false);
        }
    }

    public void Interact()
    {
        DialogueReader.reader.StartDialogue(dialogues[i]);

        if (dialogues[i + 1]) i++;
    }
}
