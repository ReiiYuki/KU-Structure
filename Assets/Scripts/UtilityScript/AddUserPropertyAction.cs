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
            if (PlayerPrefs.GetString("UTPROP").Split(null).Length > 10)
                savedData = string.Join(" ",new List<string>(PlayerPrefs.GetString("UTPROP").Split(null)).GetRange(1, PlayerPrefs.GetString("UTPROP").Split(null).Length - 1).ToArray());
            savedData += " " + e + "," + i + " ";
            PlayerPrefs.SetString("UTPROP", savedData);
        }
        else
        {
            string savedData = PlayerPrefs.GetString("UPROP");
            if (PlayerPrefs.GetString("UPROP").Split(null).Length > 10)
                savedData = string.Join(" ", new List<string>(PlayerPrefs.GetString("UPROP").Split(null)).GetRange(1, PlayerPrefs.GetString("UPROP").Split(null).Length - 1).ToArray());
            savedData += " " + e + "," + i + " ";
            PlayerPrefs.SetString("UPROP", savedData);
        }
        
        
        pane.GetComponent<LoadElementTypeAction>().LoadElement();
    }
}
