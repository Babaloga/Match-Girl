using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FamilyResources : MonoBehaviour {

    public bool alive = true;
    public bool fed = false;
    public bool medicated = false;

    public int costToFeed = 3;

    public HungerLevel hunger = HungerLevel.Satisfied;
    public SicknessLevel sickness = SicknessLevel.Healthy;

    public float sicknessRiskFactor = 0;

    public void DetermineSickness()
    {
        float rand = Random.Range(0f, 100f);

        if(rand > sicknessRiskFactor && sickness != SicknessLevel.Healthy)
        {
            sickness -= 1;
        }
        else if(rand < sicknessRiskFactor && sickness != SicknessLevel.VerySick)
        {
            sickness += 1;
        }
    }

    public void DetermineHunger()
    {
        if(fed && hunger != HungerLevel.Satisfied)
        {
            hunger -= 1;
            if (hunger != HungerLevel.Satisfied) hunger -= 1;
        }

        if(!fed)
        {
            if(hunger == HungerLevel.Starving)
            {
                alive = false;
            }
            else
            {
                hunger += 1;
            }
        }
    }

    public virtual float DetermineSicknessRisk()
    {
        return Mathf.Clamp(60f - HomeResources.temperature, 0, 100);
    }

    public Toggle food;
    public Toggle medicine;

    protected virtual void Update()
    {
        if(sickness == SicknessLevel.Healthy)
        {
            medicine.isOn = false;
            medicine.interactable = false;
        }
        else
        {
            if (PersistentGameManager.persistentStats.tonics < 1 && medicine.isOn == false)
            {
                medicine.interactable = false;
            }
            else
            {
                medicine.interactable = true;
            }
        }

        if(costToFeed > PersistentGameManager.persistentStats.money && food.isOn == false)
        {
            food.interactable = false;
        }
        else
        {
            food.interactable = true;
        }

        if (fed)
        {
            GetComponent<Image>().color = Color.green;
        }
        else
        {
            GetComponent<Image>().color = Color.red;
        }
    }

    public void FoodToggle(bool set)
    {
        if (set == true)
        {
            PersistentGameManager.persistentStats.money -= costToFeed;
            fed = true;
        }
        else
        {
            PersistentGameManager.persistentStats.money += costToFeed;
            fed = false;
        }
    }

    public void MedicineToggle(bool set)
    {
        if (set == true)
        {
            PersistentGameManager.persistentStats.tonics -= 1;
            medicated = true;
        }
        else
        {
            PersistentGameManager.persistentStats.tonics += 1;
            medicated = false;
        }
    }
}

public enum HungerLevel { Satisfied, Hungry, VeryHungry, Starving }
public enum SicknessLevel { Healthy, Sick, VerySick }
