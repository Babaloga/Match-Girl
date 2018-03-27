using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResourceManager : MonoBehaviour {


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

    public Text money;

    private int moneyLeft;


	// Use this for initialization
	void Start () {
		row1.text = "0";
		row2.text = "0";
		row3.text = "0";
		row4.text = "0";
		row5.text = "0";
		row6.text = "0";

        /*
        price1.text = "2";
        price2.text = "3";
        price3.text = "4";
        price4.text = "5";
        price5.text = "6";
        price6.text = "7";
        */


        moneyLeft = PlayerStatsManager.money;
        //moneyLeft = 100;
        //money.text = moneyLeft.ToString();

	}

    public void done()
    {
        PersistentGameManager.money = moneyLeft;

        PersistentGameManager.instance.LoadMainScene();
    }

	
	// Update is called once per frame
	void Update () {
        //money.text = moneyLeft.ToString();
    }

	public void incrementRow1(){

        int intPrice1 = Convert.ToInt32(price1.text);
        if (moneyLeft > 0 && moneyLeft > intPrice1-1)
        {
            int previous = Convert.ToInt32(row1.text);
            previous ++;
            row1.text = previous.ToString();
            moneyLeft-= intPrice1;
        }

	}

	public void decrementRow1(){

        int intPrice1 = Convert.ToInt32(price1.text);
        int previous = Convert.ToInt32 (row1.text);
        if (previous > 0)
        {
            previous--;
            moneyLeft += intPrice1;
        }
		row1.text = previous.ToString ();


	}
    public void incrementRow2()
    {
        int intPrice2 = Convert.ToInt32(price2.text);
        if (moneyLeft > 0 && moneyLeft > intPrice2 - 1)
        {
            int previous = Convert.ToInt32(row2.text);
            previous++;
            row2.text = previous.ToString();
            moneyLeft -= intPrice2;
        }
    

    }

	public void decrementRow2(){

        int intPrice2 = Convert.ToInt32(price2.text);
        int previous = Convert.ToInt32 (row2.text);
        if (previous > 0)
        {
            previous--;
            moneyLeft += intPrice2;
        }
        row2.text = previous.ToString ();
	}
	public void incrementRow3(){

        int intPrice3 = Convert.ToInt32(price3.text);
        if (moneyLeft > 0 && moneyLeft > intPrice3 - 1)
        {
            int previous = Convert.ToInt32(row3.text);
            previous++;
            row3.text = previous.ToString();
            moneyLeft -= intPrice3;
        }

	}

	public void decrementRow3(){

        int intPrice3 = Convert.ToInt32(price3.text);
        int previous = Convert.ToInt32 (row3.text);
        if (previous > 0)
        {
            previous--;
            moneyLeft += intPrice3;
        }
        row3.text = previous.ToString ();

	}
	public void incrementRow4(){

        int intPrice4 = Convert.ToInt32(price4.text);
        if (moneyLeft > 0 && moneyLeft > intPrice4 - 1)
        {
            int previous = Convert.ToInt32(row4.text);
            previous++;
            row4.text = previous.ToString();
            moneyLeft -= intPrice4;
        }

	}

	public void decrementRow4(){

        int intPrice4 = Convert.ToInt32(price4.text);
        int previous = Convert.ToInt32 (row4.text);
        if (previous > 0)
        {
            previous--;
            moneyLeft += intPrice4;
        }
        row4.text = previous.ToString ();

	}
	public void incrementRow5(){

        int intPrice5 = Convert.ToInt32(price5.text);
        if (moneyLeft > 0 && moneyLeft > intPrice5 - 1)
        {
            int previous = Convert.ToInt32(row5.text);
            previous++;
            row5.text = previous.ToString();
            moneyLeft -= intPrice5;
        }

	}

	public void decrementRow5(){

        int intPrice5 = Convert.ToInt32(price5.text);
        int previous = Convert.ToInt32 (row5.text);
        if (previous > 0)
        {
            previous--;
            moneyLeft += intPrice5;
        }
        row5.text = previous.ToString ();


	}
	public void incrementRow6(){

        int intPrice6 = Convert.ToInt32(price6.text);
        if (moneyLeft > 0 && moneyLeft > intPrice6 - 1)
        {
            int previous = Convert.ToInt32(row6.text);
            previous++;
            row6.text = previous.ToString();
            moneyLeft -= intPrice6;
        }

	}

	public void decrementRow6(){

        int intPrice6 = Convert.ToInt32(price6.text);
        int previous = Convert.ToInt32 (row6.text);
        if (previous > 0)
        {
            previous--;
            moneyLeft += intPrice6;
        }
        row6.text = previous.ToString ();

	}


}
