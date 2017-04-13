using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddUniformLoadAction : MonoBehaviour {

    public GameObject loadText, elementInput;

    public void AddUniformLoad()
    {
        float load = float.Parse(loadText.GetComponent<Text>().text);
        int element = elementInput.GetComponent<LoadInputValue>().index;
        GameObject.FindObjectOfType<BeamCollector>().AddUniformLoad(element, load);
    }
}
