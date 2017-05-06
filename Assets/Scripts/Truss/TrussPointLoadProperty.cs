using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrussPointLoadProperty : MonoBehaviour {

	public float load;
    public char axis; 
	public int node;
    public GameObject nodeG;
    public TextMesh text;
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
			transform.position = new Vector3(transform.position.x - 2.75f, transform.position.y );
			transform.Rotate (new Vector3 (0, 0, 180));
		}
	}
    public void InverseForce()
    {
        transform.localScale = new Vector3(0.2779045f, 0.2779045f, 0.2779045f);
        Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
        Debug.Log(transform.localScale.y);
        Debug.Log(transform.localScale.x);
        if (axis == 'y' && load > 0)
        {
            
            transform.localScale = new Vector3(0.2779045f, -0.2779045f, 0.2779045f);
            transform.GetChild(0).localScale = new Vector3(1, -1,1);
            transform.position = new Vector3(nodeG.GetComponent<TrussNodeProperty>().x, nodeG.GetComponent<TrussNodeProperty>().y + 2.25f);
            Debug.Log("+y: "+load);
        }
        if (axis == 'y' && load < 0)
        {
            transform.localScale = new Vector3(0.2779045f, 0.2779045f, 0.2779045f);
            transform.GetChild(0).localScale = new Vector3(1, 1, 1);
            transform.position = new Vector3(nodeG.GetComponent<TrussNodeProperty>().x, nodeG.GetComponent<TrussNodeProperty>().y - 2.25f);
            Debug.Log("-y: " + load);
        }
        if (axis == 'x' && load > 0)
        {
            transform.localScale = new Vector3(0.2779045f, -0.2779045f, 0.2779045f);
            transform.GetChild(0).localScale = new Vector3(1 , -1, -1);
            transform.position = new Vector3(nodeG.GetComponent<TrussNodeProperty>().x + 2.25f, nodeG.GetComponent<TrussNodeProperty>().y);
            //transform.Rotate(new Vector3(0, 0, 180));
            Debug.Log("x: " + load);
        }
        if (axis == 'x' && load < 0)
        {
            transform.localScale = new Vector3(-0.2779045f, 0.2779045f, 0.2779045f);
            transform.GetChild(0).localScale = new Vector3(1 ,-1, -1);
            transform.position = new Vector3(nodeG.GetComponent<TrussNodeProperty>().x - 2.25f, nodeG.GetComponent<TrussNodeProperty>().y);
            //transform.Rotate(new Vector3(0, 0, 180));
            Debug.Log("x: " + load);
        }
    }

}
