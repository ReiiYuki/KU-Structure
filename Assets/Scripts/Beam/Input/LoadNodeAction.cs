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
        string[] data = new string[nodes.Count];
        for (int i = 0; i < nodes.Count; i++)
        {
            data[i] = nodes[i].GetComponent<NodeProperty>().number + "";
        }
        GetComponent<LoadInputValue>().UpdateData(data);
    }
}
