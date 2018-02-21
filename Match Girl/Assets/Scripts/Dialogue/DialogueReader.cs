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
        Entry selectedEntry = dialogue.entries[currentValue + selection];

        ApplyModifiers(selectedEntry);

        if (selectedEntry.end)
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
            Entry entry = dialogue.entries[valueOptions[i]];

            string displayString = TextWithModifiers(entry);

            optionObjects[i].GetComponentInChildren<Text>().text = displayString;
            optionObjects[i].SetActive(true);
        }

        displayHolder.alpha = 1;
        displayHolder.interactable = true;
    }

    //Showing stat modifiers in the dialogue

    private string TextWithModifiers(Entry _entry)
    {
        string text = _entry.entryText + " ";

        if (_entry.modifyTemperature != 0)
        {
            text = text + "[Warmth " + _entry.modifyTemperature.ToString("+0;-#") + "]";
        }

        if (_entry.modifyHunger != 0)
        {
            text = text + "[Food " + _entry.modifyHunger.ToString("+0;-#") + "]";
        }

        if (_entry.modifyTime != 0)
        {
            text = text + "[Time " + _entry.modifyTime.ToString("+0;-#") + "]";
        }

        if (_entry.modifyMatches != 0)
        {
            text = text + "[Matches " + _entry.modifyMatches.ToString("+0;-#") + "]";
        }

        if (_entry.modifyMoney != 0)
        {
            text = text + "[Money " + _entry.modifyMoney.ToString("+0;-#") + "]";
        }

        return text;
    }

    private void ApplyModifiers(Entry _entry)
    {
        if (_entry.modifyTemperature != 0)
        {
            PlayerStatsManager.SetTemperature(PlayerStatsManager.Warmth + _entry.modifyTemperature);
        }

        if (_entry.modifyHunger != 0)
        {
            PlayerStatsManager.hunger += _entry.modifyHunger;
        }

        if (_entry.modifyTime != 0)
        {
            DayNightCycle.SetTime(DayNightCycle.currentTime + _entry.modifyTime);
        }

        if (_entry.modifyMatches != 0)
        {
            PlayerStatsManager.matches += _entry.modifyMatches;
        }

        if (_entry.modifyMoney != 0)
        {
            PlayerStatsManager.money += _entry.modifyMoney;
        }
    }
}
