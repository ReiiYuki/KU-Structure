using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointLoadPanelValidator : MonoBehaviour {

    public GameObject loadXText,loadYText, nodeSelector, addButton;
	
	// Update is called once per frame
	void Update () {
        float x, y;
        if (nodeSelector.GetComponent<LoadInputValue>().list.Length == 0||!float.TryParse(loadXText.GetComponent<InputField>().text,out x)||!float.TryParse(loadYText.GetComponent<InputField>().text, out y))
            addButton.GetComponent<Button>().interactable = false;
        else
            addButton.GetComponent<Button>().interactable = true;
    }
}
