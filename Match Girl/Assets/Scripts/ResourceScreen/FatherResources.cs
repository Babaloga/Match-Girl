using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FatherResources : FamilyResources {

    public bool bandaged = true;
    public bool bandagedPrevious = false;

    protected override void Awake()
    {
        alive = PersistentGameManager.father_alive;
        fed = PersistentGameManager.father_fed;
        medicated = PersistentGameManager.father_medicated;

        bandaged = PersistentGameManager.father_bandaged;

        hunger = PersistentGameManager.dadHunger;
        sickness = PersistentGameManager.dadSickness;

        bandagedPrevious = bandaged;
        bandaged = false;
    }

    protected override float DetermineSicknessRisk()
    {
        float baseValue = base.DetermineSicknessRisk();

        if(!bandaged && !bandagedPrevious)
        {
            baseValue += 50;
        }

        return baseValue;
    }

    public Toggle bandages;

    protected override void Update()
    {
        base.Update();

        if (PersistentGameManager.persistentStats.bandages < 1 && bandages.isOn == false)
        {
            bandages.interactable = false;
        }
        else
        {
            bandages.interactable = true;
        }

        if (alive && bandagedPrevious)
        {
            statusText.text += "\n Needs fresh bandages";

        }
    }

    public void BandageToggle(bool set)
    {
        if (set == true)
        {
            PersistentGameManager.persistentStats.bandages -= 1;
            bandaged = true;
        }
        else
        {
            PersistentGameManager.persistentStats.bandages += 1;
            bandaged = false;
        }
    }

    protected override void KeepConsistent()
    {
        PersistentGameManager.father_alive = alive;
        PersistentGameManager.father_fed = fed;
        PersistentGameManager.father_medicated = medicated;
        PersistentGameManager.father_bandaged = bandaged;
        PersistentGameManager.father_bandaged_previous = bandagedPrevious;
    }
}
