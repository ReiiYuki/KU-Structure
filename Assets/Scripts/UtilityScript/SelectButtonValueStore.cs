using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButtonValueStore : MonoBehaviour {

    public ElementStore.Element prop;
    public ElementStore.UElement uprop;
    public ElementStore.PElement pprop;
    public ElementStore.AElement aprop;
    public int state;

    private void Start()
    {
        state = -1;
    }

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

    public void SetPProp(ElementStore.PElement p)
    {
        this.pprop = p;
        state = 2;
        GetComponentInChildren<Text>().text = pprop.name;
    }

    public void SetAProp(ElementStore.AElement a)
    {
        Debug.Log(a.E+" "+a.A);
        this.aprop = a;
        state = 3;
        GetComponentInChildren<Text>().text = "E = " + aprop.E + " A = " + aprop.A;
    }
}
