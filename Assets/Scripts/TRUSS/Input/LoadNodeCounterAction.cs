﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadNodeCounterAction : MonoBehaviour {

	// Use this for initialization
	void OnEnable() {
        GetComponent<Text>().text = ""+(GameObject.FindObjectOfType<TRUSSCollector>().nodes.Count+1);
        Debug.Log(GameObject.FindObjectOfType<TRUSSCollector>().nodes.Count+1);
	}

}
