using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResourceManager : MonoBehaviour {

    public float foodPerMoney = 10;

	public Text row1;
	public Text row2;
	public Text row3;
	public Text row4;
	public Text row5;
	public Text row6;

    public Text price1;
    public Text price2;
    public Text price3;
    public Text price4;
    public Text price5;
    public Text price6;

    public int price_1;
    public int price_2;
    public int price_3;
    public int price_4;
    public int price_5;
    public int price_6;


    public Text money;

    public Text hungerText;

    private int moneyLeft;
    private float foodLevel;
    public static float variableFood;


	// Use this for initialization
	void Start () {
		row1.text = "0";
		row2.text = "0";
		row3.text = "0";
		row4.text = "0";
		row5.text = "0";
		row6.text = "0";

        
        price1.text = Currency.FormatPounds(price_1);
        price2.text = Currency.FormatPounds(price_2);
        price3.text = Currency.FormatPounds(price_3);
        price4.text = Currency.FormatPounds(price_4);
        price5.text = Currency.FormatPounds(price_5);
        price6.text = Currency.FormatPounds(price_6);


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
        PersistentGameManager.persistentStats.money = moneyLeft;

        PersistentGameManager.instance.LoadMainScene();
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
        money.text = Currency.FormatPounds(moneyLeft);

        variableFood = foodLevel + (foodPerMoney * rowOneMoney);
        CheckHunger();

        row1.text = rowOneMoney.ToString();
    }

	public void incrementRow1(){

        if (moneyLeft > 0 && rowOneMoney < price_1)
        {
            rowOneMoney++;
            moneyLeft--;
        }

	}

	public void decrementRow1(){
        if (rowOneMoney > 0)
        {
            rowOneMoney--;
            moneyLeft++;
        }

    }
    public void incrementRow2()
    {
        int intPrice2 = price_2;
        if (moneyLeft > 0 && moneyLeft > intPrice2 - 1)
        {
            int previous = Convert.ToInt32(row2.text);
            previous++;
            row2.text = previous.ToString();
            moneyLeft -= intPrice2;
        }
    

    }

	public void decrementRow2(){

        int intPrice2 = price_2;
        int previous = Convert.ToInt32 (row2.text);
        if (previous > 0)
        {
            previous--;
            moneyLeft += intPrice2;
        }
        row2.text = previous.ToString ();
	}
	public void incrementRow3(){

        int intPrice3 = price_3;
        if (moneyLeft > 0 && moneyLeft > intPrice3 - 1)
        {
            int previous = Convert.ToInt32(row3.text);
            previous++;
            row3.text = previous.ToString();
            moneyLeft -= intPrice3;
        }

	}

	public void decrementRow3(){

        int intPrice3 = price_3;
        int previous = Convert.ToInt32 (row3.text);
        if (previous > 0)
        {
            previous--;
            moneyLeft += intPrice3;
        }
        row3.text = previous.ToString ();

	}
	public void incrementRow4(){

        int intPrice4 = price_4;
        if (moneyLeft > 0 && moneyLeft > intPrice4 - 1)
        {
            int previous = Convert.ToInt32(row4.text);
            previous++;
            row4.text = previous.ToString();
            moneyLeft -= intPrice4;
        }

	}

	public void decrementRow4(){

        int intPrice4 = price_4;
        int previous = Convert.ToInt32 (row4.text);
        if (previous > 0)
        {
            previous--;
            moneyLeft += intPrice4;
        }
        row4.text = previous.ToString ();

	}
	public void incrementRow5(){

        int intPrice5 = price_5;
        if (moneyLeft > 0 && moneyLeft > intPrice5 - 1)
        {
            int previous = Convert.ToInt32(row5.text);
            previous++;
            row5.text = previous.ToString();
            moneyLeft -= intPrice5;
        }

	}

	public void decrementRow5(){

        int intPrice5 = price_5;
        int previous = Convert.ToInt32 (row5.text);
        if (previous > 0)
        {
            previous--;
            moneyLeft += intPrice5;
        }
        row5.text = previous.ToString ();


	}
	public void incrementRow6(){

        int intPrice6 = price_6;
        if (moneyLeft > 0 && moneyLeft > intPrice6 - 1)
        {
            int previous = Convert.ToInt32(row6.text);
            previous++;
            row6.text = previous.ToString();
            moneyLeft -= intPrice6;
        }

	}

	public void decrementRow6(){

        int intPrice6 = price_6;
        int previous = Convert.ToInt32 (row6.text);
        if (previous > 0)
        {
            previous--;
            moneyLeft += intPrice6;
        }
        row6.text = previous.ToString ();

	}


}
