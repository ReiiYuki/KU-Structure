using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCollector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddMember(float span,float type)
    {
        Debug.Log("Add Member { "+"Span : "+span+" Type : "+type+" }");
    }

    public void AddSupport(int type,int node)
    {
        Debug.Log("Add Support { " + "type : " + type + " Node : " + node + " }");
    }

    public void AddPointLoad(int node, float load)
    {
        Debug.Log("Add Point Load { " + "node : " + node + " load : " + load + " }");
    }

    public void AddUniformLoad(int element,float load)
    {
        Debug.Log("Add Uniform Load { " + "load : " + load + " element : " + element + " }");
    }

    public void AddMomentum(int node,float momentum)
    {
        Debug.Log("Add Momentum { " + "Momentum : " + momentum + " node : " + node + "} ");
    }
}
