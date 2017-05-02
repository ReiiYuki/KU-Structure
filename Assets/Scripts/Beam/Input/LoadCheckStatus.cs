using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCheckStatus : MonoBehaviour {

    public GameObject addButton, loadText, nodeSelector;
	
	// Update is called once per frame
	void Update () {
        CheckAndUpdate();
    }

    void CheckAndUpdate()
    {
        float load;
        loadText.GetComponent<Text>().resizeTextForBestFit = false;
        loadText.GetComponent<Text>().resizeTextForBestFit = true;
        if (float.TryParse(loadText.GetComponent<Text>().text, out load) && nodeSelector.GetComponent<LoadInputValue>().list.Length >= 0)
            addButton.GetComponent<Button>().interactable = true;
        else
            addButton.GetComponent<Button>().interactable = false;
    }
}
