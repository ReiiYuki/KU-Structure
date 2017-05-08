using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTRUSSMemberAction : MonoBehaviour {

    public GameObject node1Input, node2Input, propertyInput;
	
    public void AddMember()
    {
        int node1 = node1Input.GetComponent<LoadInputValue>().index;
        int node2 = node2Input.GetComponent<LoadInputValue>().index;
        int property = 0;
        SelectButtonValueStore val = propertyInput.GetComponent<SelectButtonValueStore>();
        GameObject.FindObjectOfType<TRUSSCollector>().AddMember(node1, node2, property,val.aprop,val.prop,val.pprop,val.uprop);
    }

}
