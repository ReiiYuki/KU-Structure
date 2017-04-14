using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTRUSSSupportAction : MonoBehaviour {

    public GameObject typeInput, nodeInput;

    public void AddSupport()
    {
        int type = typeInput.GetComponent<LoadInputValue>().index;
        int node = nodeInput.GetComponent<LoadInputValue>().index;
        GameObject.FindObjectOfType<TRUSSCollector>().AddSupport(type, node);
    }
}
