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
	}

    public void done()
    {
 

        PersistentGameManager.instance.LoadIntermediateScene();
    }

    public int rowOneMoney;

    void Update () {
        money.text = Currency.FormatPounds(PersistentGameManager.persistentStats.money);

        variableFood = foodLevel + (foodPerMoney * rowOneMoney);
    }

}
