using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadNodeAction : MonoBehaviour {

    void OnEnable()
    {
        LoadNode();
    }

    void LoadNode()
    {
        Debug.Log("Load Node is working");
        List<GameObject> nodes = GameObject.FindObjectOfType<BeamCollector>().nodes;
        Dropdown nodeDropdown = GetComponent<Dropdown>();
        nodeDropdown.ClearOptions();
        foreach (GameObject node in nodes)
        {
            nodeDropdown.options.Add(new Dropdown.OptionData() { text = node.GetComponent<NodeProperty>().number + "" });
        }
        nodeDropdown.RefreshShownValue();
    }
}
