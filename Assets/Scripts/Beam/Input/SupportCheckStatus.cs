using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupportCheckStatus : MonoBehaviour {

    public GameObject typeSelector, nodeSelector, addButton;
	
	// Update is called once per frame
	void Update () {
        if (typeSelector.GetComponent<LoadInputValue>().list.Length == 0 || nodeSelector.GetComponent<LoadInputValue>().list.Length == 0)
            addButton.GetComponent<Button>().interactable = false;
        else
            addButton.GetComponent<Button>().interactable = true;

    }
}
