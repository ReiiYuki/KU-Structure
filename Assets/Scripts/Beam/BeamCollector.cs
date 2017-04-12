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
}
