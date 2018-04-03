using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency {

    public static string FormatPounds(int _pence)
    {
        int pounds = (_pence - (_pence % 240)) / 240;
        _pence = _pence % 240;

        int shillings = (_pence - (_pence % 12)) / 12;
        _pence = _pence % 12;

        string returnString = "";

        if (pounds > 0) returnString += "£" + pounds + " ";
        if (shillings > 0) returnString += shillings + "s ";

        returnString += _pence + "p ";

        return returnString;
    }

}
