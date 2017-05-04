using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MemberPanelValidator : MonoBehaviour {

    public GameObject node1, node2, prop,add;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (node1.GetComponent<LoadInputValue>().index==node2.GetComponent<LoadInputValue>().index||node1.GetComponent<LoadInputValue>().list.Length == 0 || node2.GetComponent<LoadInputValue>().list.Length == 0 || prop.GetComponent<SelectButtonValueStore>().state == -1)
            add.GetComponent<Button>().interactable = false;
        else
            add.GetComponent<Button>().interactable = true;

	}
}
