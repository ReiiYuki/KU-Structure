using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddPointLoadAction : MonoBehaviour {

    public GameObject nodeInput, loadText;

    public void AddPointLoad()
    {
        int node = nodeInput.GetComponent<LoadInputValue>().index;
        float load = float.Parse(loadText.GetComponent<InputField>().text);
        GameObject.FindObjectOfType<BeamCollector>().AddPointLoad(node, load);
    }
}
