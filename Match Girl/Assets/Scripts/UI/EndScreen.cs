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
			text.text += "Phoebe's father made it through the week. With time, he found work breaking rocks for a large-scale road paving initiative.\n";
        }
        else
        {
            text.text += "Phoebe's father succumbed to infection.\n";
        }
        if (broAlive)
        {
			text.text += "Her younger brother survived, and in the days that followed found apprenticeship with a kindly cobbler down the road.\n";
        }
        else
        {
            text.text += "Her younger brother fell ill shortly after their father, and passed away quietly during the night.\n";
        }
        if (sisAlive)
        {
			text.text += "Their older sister endured the Match Girl Strike of 1888 and resumed work with a higher salary and better workplace conditions.\n";
        }
        else
        {
            text.text += "Their sister never saw the conclusion of the Match Girl Strike of 1888.\n";
        }

		text.text += "Phoebe worked as a match peddler until her twelfth birthday, when she too joined the Bryant & May match factory girls.";
    }
}
