using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResourceManager : MonoBehaviour {

    public float foodPerMoney = 10;



    public Text money;

    public Text hungerText;

    private int moneyLeft;
    private float foodLevel;
    public static float variableFood;


	// Use this for initialization
	void Start () {



        if (!PersistentGameManager.instance)
        {
            moneyLeft = 100;
            foodLevel = 100;
        }
        else
        {
            moneyLeft = PersistentGameManager.persistentStats.money;
            foodLevel = PersistentGameManager.persistentStats.food;
        }

        
        variableFood = foodLevel;
        CheckHunger();
	}

    public void done()
    {
 

        PersistentGameManager.instance.LoadIntermediateScene();
    }

	private void CheckHunger()
    {
        //print(variableFood);
        if (variableFood > 75)
        {
            hungerText.text = "Satisfied";
        }
        else if(variableFood > 50 && variableFood < 76)
        {
            hungerText.text = "Hungry";
        }
        else if(variableFood > 25 && variableFood < 51)
        {
            hungerText.text = "Famished";
        }
        else if (variableFood < 26)
        {
            hungerText.text = "Starving";
        }
    }

    public int rowOneMoney;

    void Update () {
        money.text = Currency.FormatPounds(PersistentGameManager.persistentStats.money);

        variableFood = foodLevel + (foodPerMoney * rowOneMoney);
        CheckHunger();
    }

}
