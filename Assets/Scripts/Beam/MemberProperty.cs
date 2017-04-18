using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberProperty : MonoBehaviour {
    public float length,origin;
    public int number,type;
    public UniformLoadProperty uniformLoad;
    public NodeProperty node1,node2;

    float[] E = { 1,200000000,30000000, 30000000 };
    float[] I = { 1,7*Mathf.Pow(10,-4) , 0.00048f,0.00072f};

    public float GetI()
    {
        return I[type];
    }

    public float GetE()
    {
        return E[type];
    }
}
