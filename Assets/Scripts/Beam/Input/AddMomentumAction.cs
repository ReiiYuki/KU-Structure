using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddMomentumAction : MonoBehaviour {

    public GameObject nodeDropdown, momentumText;

    public void AddMomentum()
    {
        int node = nodeDropdown.GetComponent<Dropdown>().value;
        float momentum = float.Parse(momentumText.GetComponent<Text>().text);
        GameObject.FindObjectOfType<BeamCollector>().AddMomentum(node, momentum);
    }
}
