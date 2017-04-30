using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MemberCheckStatus : MonoBehaviour {

    public GameObject spanText, propButton;
    public GameObject addButton;

    void Update()
    {
        CheckStatus();
    }

    public void CheckStatus()
    {
        if (IsReady()){
            addButton.GetComponent<Button>().interactable = true;
        }else
        {
            addButton.GetComponent<Button>().interactable = false;
        }
    }

    public bool IsReady()
    {
        float span;
        return float.TryParse(spanText.GetComponent<Text>().text, out span) && (propButton.GetComponent<SelectButtonValueStore>().prop.name != null || propButton.GetComponent<SelectButtonValueStore>().uprop.E !=0);
    }
}
