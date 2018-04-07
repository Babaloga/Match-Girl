using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugPersistentSceneLoader : MonoBehaviour {

    private void Awake()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if(SceneManager.GetSceneByBuildIndex(i).name == "Persistent")
            {
                print("Persistent already loaded");
                Destroy(gameObject);
                break;
            }
        }
        SceneManager.LoadScene("Persistent", LoadSceneMode.Additive);
        PersistentGameManager.debugMode = true;
    }

}
