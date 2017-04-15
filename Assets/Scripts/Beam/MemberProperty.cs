using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberProperty : MonoBehaviour {
    public float length,origin;
    public int number,type;
    public UniformLoadProperty uniformLoad;
    public NodeProperty node1,node2;

    float[] E = { 1 , 30 ,29000};
    float[] I = { 1 , 4.8f*Mathf.Pow(10,-3),510};

    public float GetI()
    {
        return I[type];
    }

    public float GetE()
    {
        return E[type];
    }
}
