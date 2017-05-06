using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddNodeAction : MonoBehaviour {

    public GameObject textX, textY;

	public void AddNode()
    {
        float x = float.Parse(textX.GetComponent<Text>().text);
        float y = float.Parse(textY.GetComponent<Text>().text);
        GameObject.FindObjectOfType<TRUSSCollector>().AddNode(x, y);
    }
}
