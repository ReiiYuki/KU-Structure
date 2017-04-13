using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddMemberAction : MonoBehaviour {

    public GameObject spanText, propertyInput;

	public void AddMember()
    {
        float span = float.Parse(spanText.GetComponent<Text>().text);
        int type = propertyInput.GetComponent<LoadInputValue>().index;
        GameObject.FindObjectOfType<BeamCollector>().AddMember(span, type);
    }
}
