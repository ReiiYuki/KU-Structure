﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrussMemberProperty : MonoBehaviour {
    public int number, type;
    public TrussNodeProperty node1, node2;

    public ElementStore.Element prop;
    public ElementStore.UElement uprop;
    public ElementStore.PElement pprop;
    public ElementStore.AElement aprop;
    public float stress;
    public GameObject text;
    float[] E = { 29000, 29000, 29000 , 200000,10000 };
    float[] I = { 6, 8, 8 , .002f,6};

    public string GetName()
    {
        if (!prop.Equals(default(ElementStore.Element)))
        {
            return prop.name;
        }
        else if (!pprop.Equals(default(ElementStore.PElement)))
        {
            return pprop.name;
        }

         return "E = "+aprop.E + " A = " + aprop.A;

    }

    public float GetI()
    {
        if (!prop.Equals(default(ElementStore.Element)))
        {
            return prop.area;
        }else if (!pprop.Equals(default(ElementStore.PElement)))
        {
            return pprop.area/ 10000f;
        }else if (!aprop.Equals(default(ElementStore.AElement)))
        {
            return aprop.A;
        }
        return I[type];
    }

    public float GetE()
    {
        if (!prop.Equals(default(ElementStore.Element)))
        {
            return prop.e;
        }
        else if (!pprop.Equals(default(ElementStore.PElement)))
        {
            return 2.07f*Mathf.Pow(10,10);
        }
        else if (!aprop.Equals(default(ElementStore.AElement)))
        {
            return aprop.E;
        }
        return E[type];
    }
    public float GetFy()
    {
        if (!prop.Equals(default(ElementStore.Element)))
        {
            return prop.fy;
        }
        else if (!pprop.Equals(default(ElementStore.PElement)))
        {
            return pprop.fy;
        }
        return 0;
    }
    public float GetR()
    {
        if (!prop.Equals(default(ElementStore.Element)))
        {
            return prop.r;
        }
        else if (!pprop.Equals(default(ElementStore.PElement)))
        {
            return pprop.r;
        }
        return 0;
    }
    public bool CanDesignCheck()
    {
        return !prop.Equals(default(ElementStore.Element)) || !pprop.Equals(default(ElementStore.PElement));
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
