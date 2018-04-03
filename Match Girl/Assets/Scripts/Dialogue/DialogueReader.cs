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
    public Text characterNameText;

    private Text displayText;

    public GameObject[] optionObjects;
    GameObject continueObject;

    public static DialogueReader reader;

    //Default Unity Functions

    private void Start()
    {
        reader = this;
        displayText = dialogueObject.GetComponentInChildren<Text>();

        optionObjects = new GameObject[10];

        for (int i = 0; i < 10; i++)
        {
            if (optionHolder.GetChild(i))
                optionObjects[i] = optionHolder.GetChild(i).gameObject;
        }

        continueObject = optionHolder.GetChild(10).gameObject;
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
        print(selection);
        int nextValue = (currentValue * 10) + selection;

        if (!dialogue.entries.ContainsKey(nextValue) || selection > 9)
        {
            EndDialogue();
            return;
        }

        Entry selectedEntry = dialogue.entries[nextValue];

        ApplyModifiers(selectedEntry);

        currentValue = nextValue;
        GetOptions();
        ShowDialogue();
    }

    public void GetOptions()
    {
        List<int> options = new List<int>();

        for (int i = 0; i < dialogue.entries[currentValue].responses.Length; i++)
        {
            options.Add(i);
        }

        valueOptions = options;
    }

    public void EndDialogue()
    {
        displayHolder.alpha = 0;
        displayHolder.interactable = false;
        showingDialogue = false;
    }

    //Dialogue Display

    private void ShowDialogue()
    {
        showingDialogue = true;
        displayText.text = dialogue.entries[currentValue].entryText;

        if(dialogue.entries[currentValue].speakerName != "")
        {
            characterNameText.text = dialogue.entries[currentValue].speakerName;
        }
        else
        {
            characterNameText.text = dialogue.characterName;
        }

        foreach(GameObject o in optionObjects)
        {
            o.SetActive(false);
        }

        bool wayOut = false;

        Entry currentEntry = dialogue.entries[currentValue];

        for (int i = 0; i < valueOptions.Count; i++)
        {
            string optionText = currentEntry.responses[i];

            if (dialogue.entries.ContainsKey((currentValue * 10) + valueOptions[i]))
            {
                Entry nextEntry = dialogue.entries[(currentValue * 10) + valueOptions[i]];
                optionText += GetModifiers(nextEntry);

                if (PlayerStatsManager.stats.matches + nextEntry.modifyMatches < 0 || PlayerStatsManager.stats.money + nextEntry.modifyMoney < 0)
                {
                    optionObjects[i].GetComponent<Button>().interactable = false;
                }
                else
                {
                    optionObjects[i].GetComponent<Button>().interactable = true;
                    wayOut = true;
                }
            }
            else
            {
                wayOut = true;
            }

            optionObjects[i].GetComponentInChildren<Text>().text = optionText;
            optionObjects[i].SetActive(true);
        }

        if (!wayOut)
        {
            continueObject.SetActive(true);
            ContinueButton.selectionInt = dialogue.entries[currentValue].responses.Length;
        }
        else
        {
            continueObject.SetActive(false);
        }

        displayHolder.alpha = 1;
        displayHolder.interactable = true;
    }

    //Showing stat modifiers in the dialogue

    private string GetModifiers(Entry _entry)
    {
        string text = " ";

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
            if(PlayerStatsManager.stats.matches + _entry.modifyMatches < 0)
            {
                text = text + "<color=#dd0000ff>[Matches " + _entry.modifyMatches.ToString("+0;-#") + "]</color>";
            }
            else
            {
                text = text + "[Matches " + _entry.modifyMatches.ToString("+0;-#") + "]";
            }
        }

        if (_entry.modifyMoney != 0)
        {
            if (PlayerStatsManager.stats.money + _entry.modifyMoney < 0)
            {
                text = text + "<color=#dd0000ff>[Money " + _entry.modifyMoney.ToString("+0;-#") + "]</color>";
            }
            else
            {
                text = text + "[Money " + _entry.modifyMoney.ToString("+0;-#") + "]";
            }
        }

        return text;
    }

    private void ApplyModifiers(Entry _entry)
    {
        if (_entry.modifyTemperature != 0)
        {
            print("Modify Temperature: " + _entry.modifyTemperature);
            PlayerStatsManager.SetTemperature(PlayerStatsManager.Warmth + _entry.modifyTemperature);
        }

        if (_entry.modifyHunger != 0)
        {
            print("Modify Hunger: " + _entry.modifyHunger);
            PlayerStatsManager.stats.food += _entry.modifyHunger;
        }

        if (_entry.modifyTime != 0)
        {
            print("Modify Time: " + _entry.modifyTime);
            DayNightCycle.SetTime(DayNightCycle.currentTime + _entry.modifyTime);
        }

        if (_entry.modifyMatches != 0)
        {
            print("Modify TMatches: " + _entry.modifyMatches);
            PlayerStatsManager.stats.matches += _entry.modifyMatches;
        }

        if (_entry.modifyMoney != 0)
        {
            print("Modify Money: " + _entry.modifyMoney);
            PlayerStatsManager.stats.money += _entry.modifyMoney;
        }
    }
}
