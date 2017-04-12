using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddSupportAction : MonoBehaviour {

    public GameObject typeDropdown, nodeDropdown;

	public void AddSupport()
    {
        int type = typeDropdown.GetComponent<Dropdown>().value;
        int node = nodeDropdown.GetComponent<Dropdown>().value;
        GameObject.FindObjectOfType<BeamCollector>().AddSupport(type, node);
    }

}
