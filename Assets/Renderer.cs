using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renderer : MonoBehaviour {

    public GameObject member;
    float currentPosition = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateSpan(float span,int property)
    {
        GameObject mem = Instantiate(member, new Vector3(currentPosition,0 ), Quaternion.identity);
        mem.transform.localScale = new Vector3(5*span, 1, 1);
        currentPosition += span/2 ;
    }
}
