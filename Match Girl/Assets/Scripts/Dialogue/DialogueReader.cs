using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueReader : MonoBehaviour {

    public Dialogue dialogue;
    public int currentValue;
    public List<int> values;
    public List<int> valueOptions;

    public bool showingDialogue = false;

    public CanvasGroup displayHolder;
    public Transform optionHolder;

    public GameObject dialogueObject;

    private Text displayText;

    public GameObject[] optionObjects;

    //Default Unity Functions

    private void Start()
    {
        displayText = dialogueObject.GetComponent<Text>();

        optionObjects = new GameObject[9];

        for (int i = 0; i < 9; i++)
        {
            if (optionHolder.GetChild(i))
                optionObjects[i] = optionHolder.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartDialogue(dialogue);
        }
    }

    //Dialogue Backend

    public void StartDialogue(Dialogue _dialogue)
    {
        dialogue = _dialogue;
        values = new List<int>(dialogue.entries.Keys);
        currentValue = 0;
        GetOptions();
        ShowDialogue();
    }

    public void StartDialogue(Dialogue _dialogue, int start)
    {
        dialogue = _dialogue;
        values = new List<int>(dialogue.entries.Keys);
        currentValue = start;
        GetOptions();
        ShowDialogue();
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
            else ShowDialogue();
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
        displayHolder.alpha = 0;
        displayHolder.interactable = false;
    }

    //Dialogue Display

    private void ShowDialogue()
    {
        displayText.text = dialogue.entries[currentValue].entryText;

        foreach(GameObject o in optionObjects)
        {
            o.SetActive(false);
        }

        for (int i = 0; i < valueOptions.Count; i++)
        {
            optionObjects[i].GetComponentInChildren<Text>().text = dialogue.entries[valueOptions[i]].entryText;
            optionObjects[i].SetActive(true);
        }

        displayHolder.alpha = 1;
        displayHolder.interactable = true;
    }
}
