using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueReader : MonoBehaviour {

    public Dialogue dialogue;
    public int currentValue;
    public List<int> values;
    public List<int> valueOptions;

    public void StartDialogue(Dialogue _dialogue)
    {
        dialogue = _dialogue;
        values = new List<int>(dialogue.entries.Keys);
        currentValue = 0;
        GetOptions();
    }

    public void StartDialogue(Dialogue _dialogue, int start)
    {
        dialogue = _dialogue;
        values = new List<int>(dialogue.entries.Keys);
        currentValue = start;
         GetOptions();
    }

    public void Select(int selection)
    {
        if (dialogue.entries[currentValue + selection].end)
            EndDialogue();
        else
        {
            currentValue = (currentValue + selection) * 10;
            GetOptions();
            if (valueOptions.Count == 0) EndDialogue();
        }
    }

    public void GetOptions()
    {
        List<int> options = new List<int>();

        foreach (int value in values)
        {
            if (value > currentValue && value < currentValue + 10) options.Add(value);
        }

        valueOptions = options;
    }

    public void EndDialogue()
    {

    }
}
