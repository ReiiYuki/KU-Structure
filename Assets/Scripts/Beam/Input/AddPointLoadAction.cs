using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddPointLoadAction : MonoBehaviour {

    public GameObject nodeDropdown, loadText;

    public void AddPointLoad()
    {
        int node = nodeDropdown.GetComponent<Dropdown>().value;
        float load = float.Parse(loadText.GetComponent<Text>().text);
        GameObject.FindObjectOfType<BeamCollector>().AddPointLoad(node, load);
    }
}
