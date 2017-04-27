using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPropertyCheckStatus : MonoBehaviour {

    public GameObject eText, iText, addButton;
	
	// Update is called once per frame
	void Update () {
        Check();
	}

    void Check()
    {
        float e,i;
        if (float.TryParse(eText.GetComponent<Text>().text, out e) && float.TryParse(iText.GetComponent<Text>().text, out i))
            addButton.GetComponent<Button>().interactable = true;
        else
            addButton.GetComponent<Button>().interactable = false;
    }
}
