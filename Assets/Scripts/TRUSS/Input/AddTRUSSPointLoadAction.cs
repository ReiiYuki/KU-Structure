using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTRUSSPointLoadAction : MonoBehaviour {

    public GameObject nodeInput, loadXText, loadYText;

    public void AddPointLoad()
    {
        int node = nodeInput.GetComponent<LoadInputValue>().index;
        float loadX = float.Parse(loadXText.GetComponent<Text>().text);
        float loadY = float.Parse(loadYText.GetComponent<Text>().text);
        GameObject.FindObjectOfType<TRUSSCollector>().AddPointLoad(node, loadX, loadY);
		Debug.Log ("in");
    }
}
