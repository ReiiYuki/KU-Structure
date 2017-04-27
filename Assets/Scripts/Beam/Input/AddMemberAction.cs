using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddMemberAction : MonoBehaviour {

    public GameObject spanText, propertyInput;

	public void AddMember()
    {
        float span = float.Parse(spanText.GetComponent<Text>().text);
        //int type = propertyInput.GetComponent<LoadInputValue>().index;
        ElementStore.Element prop = propertyInput.GetComponent<SelectButtonValueStore>().prop;
        ElementStore.UElement uprop = propertyInput.GetComponent<SelectButtonValueStore>().uprop;
        GameObject.FindObjectOfType<BeamCollector>().AddMember(span, prop,uprop);
    }
}
