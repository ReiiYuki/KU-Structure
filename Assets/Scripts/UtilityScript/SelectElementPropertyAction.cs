using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectElementPropertyAction : MonoBehaviour {

    public ElementStore.Element prop;
    public ElementStore.UElement uprop;
    public ElementStore.PElement pprop;
    public ElementStore.AElement aprop;

	public void Select()
    {
        if (prop.name != null)
        {
            GameObject.FindObjectOfType<SelectButtonValueStore>().SetProp(prop);
            transform.parent.parent.parent.gameObject.SetActive(false);
        }else if (pprop.name!=null)
        {
            GameObject.FindObjectOfType<SelectButtonValueStore>().SetPProp(pprop);
            transform.parent.parent.parent.gameObject.SetActive(false);
        }
        else if (!aprop.Equals(default(ElementStore.AElement)))
        {
            GameObject.FindObjectOfType<SelectButtonValueStore>().SetAProp(aprop);
            transform.parent.parent.parent.parent.gameObject.SetActive(false);
        }
        else
        {
            GameObject.FindObjectOfType<SelectButtonValueStore>().SetUProp(uprop);
            transform.parent.parent.parent.parent.gameObject.SetActive(false);
        }
    }
}
