using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTRUSSNodeAction : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        List<GameObject> nodes = GameObject.FindObjectOfType<TRUSSCollector>().nodes;
        string[] data = new string[nodes.Count];
        for (int i = 0; i < nodes.Count; i++)
            data[i] = i+"";
        GetComponent<LoadInputValue>().UpdateData(data);
    }

}
