using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRUSSCollector : MonoBehaviour {

    public List<GameObject> nodes,members;

	// Use this for initialization
	void Start () {
        this.nodes = new List<GameObject>();
        this.members = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddNode(int x,int y)
    {
        Debug.Log("Add Node { x:" + x + " y : " + y + " }");
    }

    public void AddMember(int node1,int node2,int property)
    {
        Debug.Log("Add Member { node1 : " + node1 + " node2 : " + node2 + " property : " + property + " }");
    }

    public void AddSupport(int type,int node)
    {
        Debug.Log("Add Support { type : " + type + " node : " + node + " }");
    }

    public void AddPointLoad(int node,float loadX,float loadY)
    {
        Debug.Log("Add Point Load { node : " + node + " loadX : " + loadX + " loadY : " + loadY + " }");
    }
}
