using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddMomentumAction : MonoBehaviour {

    public GameObject nodeInput, momentumText;

    public void AddMomentum()
    {
        int node = nodeInput.GetComponent<LoadInputValue>().index;
        float momentum = float.Parse(momentumText.GetComponent<InputField>().text);
        GameObject.FindObjectOfType<BeamCollector>().AddMomentum(node, momentum);
    }
}
