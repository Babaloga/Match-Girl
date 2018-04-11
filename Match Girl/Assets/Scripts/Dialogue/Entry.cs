using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class should be able to hold any data we'll eventually need for dialogue entries

[System.Serializable]
public class Entry {

    public string entryText;

    public string speakerName = "";

    public float modifyTemperature = 0;
    public float modifyHunger = 0;
    public float modifyTime = 0;

    public int modifyMatches = 0;
    public int modifyMoney = 0;

    public int continueToEntry = 0;

    public string[] responses;

}
