using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectElementPropertyAction : MonoBehaviour {

    public ElementStore.Element prop;
    public ElementStore.UElement uprop;

	public void Select()
    {
        if (prop.name != null)
        {
            GameObject.FindObjectOfType<SelectButtonValueStore>().SetProp(prop);
            transform.parent.parent.parent.gameObject.SetActive(false);
        }
        else
        {
            GameObject.FindObjectOfType<SelectButtonValueStore>().SetUProp(uprop);
            transform.parent.parent.parent.parent.gameObject.SetActive(false);
        }
    }
}
