using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButtonValueStore : MonoBehaviour {

    public ElementStore.Element prop;
    public ElementStore.UElement uprop;
    public int state;
    public void SetProp(ElementStore.Element prop)
    {
        this.prop = prop;
        state = 0;
        GetComponentInChildren<Text>().text = prop.name;
    }

    public void SetUProp(ElementStore.UElement uprop)
    {
        this.uprop = uprop;
        state = 1;
        GetComponentInChildren<Text>().text = "E = "+uprop.E+" I = "+uprop.I;
    }
}
