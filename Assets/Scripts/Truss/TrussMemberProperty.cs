using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrussMemberProperty : MonoBehaviour {
    public int number, type;
    public TrussNodeProperty node1, node2;

    public ElementStore.Element prop;
    public ElementStore.UElement uprop;
    public ElementStore.PElement pprop;
    public ElementStore.AElement aprop;

    float[] E = { 29000, 29000, 29000 , 200000,10000 };
    float[] I = { 6, 8, 8 , .002f,6};

    public float GetI()
    {
        if (prop.Equals(default(ElementStore.Element)))
        {
            return prop.area/ 10000f;
        }else if (pprop.Equals(default(ElementStore.PElement)))
        {
            return pprop.area/ 10000f;
        }else if (aprop.Equals(default(ElementStore.AElement)))
        {
            return prop.area;
        }
        return I[type];
    }

    public float GetE()
    {
        if (prop.Equals(default(ElementStore.Element)))
        {
            return prop.e;
        }
        else if (pprop.Equals(default(ElementStore.PElement)))
        {
            return 2.07f*Mathf.Pow(10,10);
        }
        else if (aprop.Equals(default(ElementStore.AElement)))
        {
            return prop.e;
        }
        return E[type];
    }
    public float lenght()
    {
        return Mathf.Sqrt(Mathf.Pow(node1.transform.position.x - node2.transform.position.x, 2) + Mathf.Pow(node1.transform.position.y - node2.transform.position.y, 2)) ;
    }
    public float lenghtIn()
    {
        return Mathf.Sqrt(Mathf.Pow(node1.transform.position.x - node2.transform.position.x,2) + Mathf.Pow(node1.transform.position.y - node2.transform.position.y,2))*12;
    }
}
