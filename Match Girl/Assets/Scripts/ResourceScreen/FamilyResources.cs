using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FamilyResources : MonoBehaviour {

    public bool isPlayer = false;
    public bool alive = true;
    public bool fed = false;
    public bool medicated = false;

    public static bool playerMedicated = false;

    public int costToFeed = 3;

    public HungerLevel hunger = HungerLevel.Satisfied;
    public SicknessLevel sickness = SicknessLevel.Healthy;

    public Image portrait;

    public Sprite wellPortrait;
    public Sprite unwellPortrait;

    public Color fadedColor = Color.white;

    public GameObject infoHolder;
    public GameObject deathHolder;

    public Text statusText;

    public float sicknessRiskFactor = 0;

    protected virtual void Awake()
    {
        if (isPlayer)
        {
            playerMedicated = false;

            if (PersistentGameManager.persistentSicknessLevel == EffectLevel.High)
            {
                sickness = SicknessLevel.VerySick;
            }
            else if (PersistentGameManager.persistentSicknessLevel == EffectLevel.None)
            {
                sickness = SicknessLevel.Healthy;
            }
            else
            {
                sickness = SicknessLevel.Sick;
            }

            if(PersistentGameManager.persistentStats.food > 60)
            {
                hunger = HungerLevel.Satisfied;
            }
            else if (PersistentGameManager.persistentStats.food < 20)
            {
                hunger = HungerLevel.Starving;
            }
            else if (PersistentGameManager.persistentStats.food < 40)
            {
                hunger = HungerLevel.VeryHungry;
            }
            else
            {
                hunger = HungerLevel.Hungry;
            }
        }
        else
        {
            if (gameObject.tag == "Brother")
            {
                alive = PersistentGameManager.bro_alive;
                fed = PersistentGameManager.bro_fed;
                medicated = PersistentGameManager.bro_medicated;
                hunger = PersistentGameManager.broHunger;
                sickness = PersistentGameManager.broSickness;
            }
            else if (gameObject.tag == "Sister")
            {
                alive = PersistentGameManager.sis_alive;
                fed = PersistentGameManager.sis_fed;
                medicated = PersistentGameManager.sis_medicated;
                hunger = PersistentGameManager.sisHunger;
                sickness = PersistentGameManager.sisSickness;
            }
        }
    }

    public void DetermineSickness()
    {
        sicknessRiskFactor = DetermineSicknessRisk();

        float rand = Random.Range(0f, 100f);

        if(rand > sicknessRiskFactor && sickness != SicknessLevel.Healthy)
        {
            sickness -= 1;
        }
        else if(rand < sicknessRiskFactor)
        {
            if (sickness != SicknessLevel.VerySick)
                sickness += 1;
            else
                alive = false;
        }

        if (gameObject.tag == "Brother")
        {
            PersistentGameManager.broSickness = sickness;
        }
        else if (gameObject.tag == "Sister")
        {
            PersistentGameManager.sisSickness = sickness;
        }
        else if (gameObject.tag == "Father")
        {
            PersistentGameManager.dadSickness = sickness;
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

        if (gameObject.tag == "Brother")
        {
            PersistentGameManager.broHunger = hunger;
        }
        else if (gameObject.tag == "Sister")
        {
            PersistentGameManager.sisHunger = hunger;
        }
        else if (gameObject.tag == "Father")
        {
            PersistentGameManager.dadHunger = hunger;
        }
    }

    protected virtual float DetermineSicknessRisk()
    {
        float risk = Mathf.Clamp(70f - HomeResources.temperature, 0, 100);

        if (medicated)
        {
            risk -= 20;
        }

        return risk;
    }

    public Toggle food;
    public Toggle medicine;

    protected virtual void Update()
    {
        if (!alive)
        {
            portrait.sprite = wellPortrait;
            portrait.color = fadedColor;

            infoHolder.SetActive(false);
            deathHolder.SetActive(true);
        }
        else
        {
            statusText.text = sickness.ToString() + ", " + hunger.ToString();
        }

    if (isPlayer) playerMedicated = medicated;

        if (sickness == SicknessLevel.Healthy && hunger == HungerLevel.Satisfied)
        {
            portrait.sprite = wellPortrait;
        }
        else
        {
            portrait.sprite = unwellPortrait;
        }

        if (sickness == SicknessLevel.Healthy)
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

        if (costToFeed > PersistentGameManager.persistentStats.money && food.isOn == false)
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

        KeepConsistent();
        
    }

    public void FoodToggle(bool set)
    {
        if (set == true)
        {
            PersistentGameManager.persistentStats.money -= costToFeed;
            fed = true;
            if (isPlayer)
            {
                PersistentGameManager.persistentStats.food += 50;
            }
        }
        else
        {
            PersistentGameManager.persistentStats.money += costToFeed;
            fed = false;
            if (isPlayer)
            {
                PersistentGameManager.persistentStats.food -= 50;
            }
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

    protected virtual void KeepConsistent()
    {
        if (gameObject.tag == "Brother")
        {
            PersistentGameManager.bro_alive = alive;
            PersistentGameManager.bro_fed = fed;
            PersistentGameManager.bro_medicated = medicated;
        }
        else if (gameObject.tag == "Sister")
        {
            PersistentGameManager.sis_alive = alive;
            PersistentGameManager.sis_fed = fed;
            PersistentGameManager.sis_medicated = medicated;
        }
    }
}

public enum HungerLevel { Satisfied, Hungry, VeryHungry, Starving }
public enum SicknessLevel { Healthy, Sick, VerySick }
