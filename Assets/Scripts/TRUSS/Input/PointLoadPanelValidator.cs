using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointLoadPanelValidator : MonoBehaviour {

    public GameObject loadXText,loadYText, nodeSelector, addButton;
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Check());
        if (nodeSelector.GetComponent<LoadInputValue>().list.Length == 0||!Check())
            addButton.GetComponent<Button>().interactable = false;
        else
            addButton.GetComponent<Button>().interactable = true;
    }

    bool Check()
    {
        float x, y;
        if (float.TryParse(loadXText.GetComponent<InputField>().text, out x) && loadYText.GetComponent<InputField>().text == "")
            return true;
        else if (float.TryParse(loadYText.GetComponent<InputField>().text, out y) && loadXText.GetComponent<InputField>().text == "")
            return true;
        else if (float.TryParse(loadXText.GetComponent<InputField>().text, out x) && float.TryParse(loadYText.GetComponent<InputField>().text, out y))
            return true;
        return false;
    }
}
