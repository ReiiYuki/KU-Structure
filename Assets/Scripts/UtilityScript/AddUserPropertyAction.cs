using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddUserPropertyAction : MonoBehaviour {

    public GameObject eText, iText,pane;
    public string type;
	public void AddUserProperty()
    {
        Debug.Log(eText.GetComponent<InputField>().text + " " + iText.GetComponent<InputField>().text);
        float e = float.Parse(eText.GetComponent<InputField>().text);
        float i = float.Parse(iText.GetComponent<InputField>().text);
        if (type == "UT")
        {
            string savedData = PlayerPrefs.GetString("UTPROP");
            savedData += " " + e + "," + i + " ";
            PlayerPrefs.SetString("UTPROP", savedData);
        }
        else
        {
            string savedData = PlayerPrefs.GetString("UPROP");
            savedData += " " + e + "," + i + " ";
            PlayerPrefs.SetString("UPROP", savedData);
        }
        
        
        pane.GetComponent<LoadElementTypeAction>().LoadElement();
    }
}
