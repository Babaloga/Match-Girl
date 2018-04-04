using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugPersistentSceneLoader : MonoBehaviour {

    private void Awake()
    {
        SceneManager.LoadScene("Persistent", LoadSceneMode.Additive);
        PersistentGameManager.debugMode = true;
    }

}
