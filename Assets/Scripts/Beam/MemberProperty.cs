using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberProperty : MonoBehaviour {
    public float length,origin;
    public int number,type;
    public UniformLoadProperty uniformLoad;
    public NodeProperty node1,node2;
    public ElementStore.Element prop;
    public ElementStore.UElement uprop;
    public float stress,v,pos;

    float[] E = { 1,200000000,30000000, 30000000 , 90000000 };
    float[] I = { 1,7*Mathf.Pow(10,-4) , 0.00048f,0.00072f, 0.00048f };

    public float GetI()
    {
        if (prop.name != null) return prop.ix;
        else return uprop.I;
      //  return I[type];
    }

    public float GetE()
    {
        if (prop.name != null) return prop.e;
        else return uprop.E;
     //   return E[type];
    }

    public string GetName()
    {
        if (prop.name != null) return prop.name;
        else return uprop.E+" "+uprop.I;
    }
}
