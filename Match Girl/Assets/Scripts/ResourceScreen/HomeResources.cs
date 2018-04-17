using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeResources : MonoBehaviour {

    public static float temperature;

    public float temperaturePerLog = 5f;

    public int logs = 0;
    public int maxLogs = 8;
    public int logCost = 2;

    public Toggle lightFurnace;

    public Button logUp;
    public Button logDown;

	void Start () {
        temperature = Random.Range(15, 25);
	}

    private void Update()
    {
        if (lightFurnace.isOn)
        {
            if (logs < maxLogs && PersistentGameManager.persistentStats.money >= logCost)
            {
                logUp.interactable = true;
            }
            else
            {
                logUp.interactable = false;
            }

            if (logs > 0)
            {
                logDown.interactable = true;
            }
            else
            {
                logDown.interactable = false;
            }
        }
        else
        {
            logUp.interactable = false;
            logDown.interactable = false;
            while(logs > 0)
            {
                LogsDown();
            }
        }

        if (PersistentGameManager.persistentStats.matches <= 0 && !lightFurnace.isOn)
        {
            lightFurnace.interactable = false;
        }
        else
        {
            lightFurnace.interactable = true;
        }
    }

    public void ToggleFurnace(bool set)
    {
        if (set)
        {
            PersistentGameManager.persistentStats.matches -= 1;
        }
        else
        {
            PersistentGameManager.persistentStats.matches += 1;
        }
    }

    public void LogsUp()
    {
        logs += 1;
        temperature += temperaturePerLog;

        PersistentGameManager.persistentStats.money -= logCost;
    }

    public void LogsDown()
    {
        logs -= 1;
        temperature -= temperaturePerLog;

        PersistentGameManager.persistentStats.money += logCost;
    }
}
