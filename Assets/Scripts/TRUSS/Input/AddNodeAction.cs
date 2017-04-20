using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddNodeAction : MonoBehaviour {

    public GameObject textX, textY;

	public void AddNode()
    {
        int x = int.Parse(textX.GetComponent<Text>().text);
        int y = int.Parse(textY.GetComponent<Text>().text);
        GameObject.FindObjectOfType<TRUSSCollector>().AddNode(x, y);
    }
}
