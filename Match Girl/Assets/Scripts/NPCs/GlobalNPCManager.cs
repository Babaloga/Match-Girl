using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalNPCManager : MonoBehaviour {

    public int minimumNPCCount = 50;

    int currentNPCCount = 0;

    public GameObject maleNPCPrefab;
    public GameObject femaleNPCPrefab;

    private void Start()
    {
        UpdateNPCCount();
    }

    private void Update()
    {
        if (Time.frameCount % 60 == 0) UpdateNPCCount();
    }

    void UpdateNPCCount()
    {
        currentNPCCount = FindObjectsOfType<MoveTo>().Length;

        print(currentNPCCount);

        if(currentNPCCount < minimumNPCCount)
        {
            StartCoroutine(SpawnNPCs(minimumNPCCount - currentNPCCount));
        }
    }

    IEnumerator SpawnNPCs (int count)
    {
        print(count);

        for (int i = 0; i < count; i++)
        {
            if (Random.Range(0, 2) > 0) Instantiate(maleNPCPrefab);
            else Instantiate(femaleNPCPrefab);

            yield return null;
        }
    }

}
