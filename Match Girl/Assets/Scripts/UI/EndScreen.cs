using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

    public Text text;

    bool dadAlive;
    bool broAlive;
    bool sisAlive;

    private void Start()
    {
        dadAlive = PersistentGameManager.father_alive;
        broAlive = PersistentGameManager.bro_alive;
        sisAlive = PersistentGameManager.sis_alive;

        text.text = "";

        if (dadAlive)
        {
            text.text += "Your father survived./n";
        }
        else
        {
            text.text += "Your father did not survive./n";
        }
        if (broAlive)
        {
            text.text += "Your brother survived./n";
        }
        else
        {
            text.text += "Your brother did not survive./n";
        }
        if (sisAlive)
        {
            text.text += "Your sister survived./n";
        }
        else
        {
            text.text += "Your sister did not survive./n";
        }

    }
}
