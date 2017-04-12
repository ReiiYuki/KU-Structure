using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddMemberAction : MonoBehaviour {

    public GameObject spanText, typeDropdown;

	public void AddMember()
    {
        float span = float.Parse(spanText.GetComponent<Text>().text);
        int type = typeDropdown.GetComponent<Dropdown>().value;
        GameObject.FindObjectOfType<BeamCollector>().AddMember(span, type);
    }
}
