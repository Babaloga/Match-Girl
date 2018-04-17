using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FatherResources : FamilyResources {

    public bool bandaged = true;
    private bool bandagedPrevious = false;

    private void Awake()
    {
        bandagedPrevious = bandaged;
        bandaged = false;
    }

    public override float DetermineSicknessRisk()
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

        PersistentGameManager.instance.father_bandaged = bandaged;
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
}
