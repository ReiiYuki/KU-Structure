using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddMemberAction : MonoBehaviour {

    public GameObject SpaneText, Property;

	public void AddMember()
    {
        float span = float.Parse(SpaneText.GetComponent<Text>().text);
        int property = Property.GetComponent<Dropdown>().value;
        GameObject.FindObjectOfType<Renderer>().CreateSpan(span, property);
    }

}
