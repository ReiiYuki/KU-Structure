using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrussPointLoadProperty : MonoBehaviour {

	public float load;
    public char axis; 
	public int node;

	public void Inverse()
	{
		if (axis == 'y' && load > 0)
		{
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
			transform.GetChild(0).localScale = new Vector3(transform.GetChild(0).localScale.x, transform.GetChild(0).localScale.y * -1, transform.GetChild(0).localScale.z);
			transform.position = new Vector3(transform.position.x, transform.position.y - 2.5f);
		}
		if (axis == 'x' && load > 0)
		{
			transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y , transform.localScale.z);
			transform.GetChild(0).localScale = new Vector3(transform.GetChild(0).localScale.x * -1, transform.GetChild(0).localScale.y, transform.GetChild(0).localScale.z);
			transform.position = new Vector3(transform.position.x - 2.5f, transform.position.y );
			transform.Rotate (new Vector3 (0, 0, 180));
			Debug.Log ("in");
		}
	}
}
