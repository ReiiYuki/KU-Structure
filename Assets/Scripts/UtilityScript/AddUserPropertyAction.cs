using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddUserPropertyAction : MonoBehaviour {

    public GameObject eText, iText,pane;

	public void AddUserProperty()
    {
        Debug.Log(eText.GetComponent<Text>().text + " " + iText.GetComponent<Text>().text);
        float e = float.Parse(eText.GetComponent<Text>().text);
        float i = float.Parse(iText.GetComponent<Text>().text);
        string savedData = PlayerPrefs.GetString("UPROP");
        savedData += " " + e + "," + i + " ";
        PlayerPrefs.SetString("UPROP", savedData);
        pane.GetComponent<LoadElementTypeAction>().LoadElement();
    }
}
