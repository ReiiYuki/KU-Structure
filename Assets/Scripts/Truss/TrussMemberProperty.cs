using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrussMemberProperty : MonoBehaviour {
    public int number, type;
    public TrussNodeProperty node1, node2;

    float[] E = { 29000, 29000, 29000 };
    float[] I = { 6, 8, 8 };

    public float GetI()
    {
        return I[type];
    }

    public float GetE()
    {
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
