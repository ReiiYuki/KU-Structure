using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteractiveManager : MonoBehaviour {

    public GameObject buttonPanel,analyzePanel,toolbox;

	public void PausePanel()
    {
        foreach (Button button in buttonPanel.GetComponentsInChildren<Button>())
            button.interactable = false;
        foreach (Button button in analyzePanel.GetComponentsInChildren<Button>())
            button.interactable = false;
        foreach (Button button in toolbox.GetComponentsInChildren<Button>())
            button.interactable = false;
    }

    public void ActivatePanel()
    {
        foreach (Button button in buttonPanel.GetComponentsInChildren<Button>())
            button.interactable = true;
        foreach (Button button in analyzePanel.GetComponentsInChildren<Button>())
            button.interactable = true;
        foreach (Button button in toolbox.GetComponentsInChildren<Button>())
            button.interactable = true;
    }
}
