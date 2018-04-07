using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyObject : MonoBehaviour {

    public bool[] appearOnDays;

	void Start () {

        int day = PersistentGameManager.currentDay;

        if(appearOnDays.Length > day && appearOnDays[day] == true)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
	}
}
