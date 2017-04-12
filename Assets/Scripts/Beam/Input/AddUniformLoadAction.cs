using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddUniformLoadAction : MonoBehaviour {

    public GameObject loadText, elementDropdown;

    public void AddUniformLoad()
    {
        float load = float.Parse(loadText.GetComponent<Text>().text);
        int element = elementDropdown.GetComponent<Dropdown>().value;
        GameObject.FindObjectOfType<BeamCollector>().AddUniformLoad(element, load);
    }
}
