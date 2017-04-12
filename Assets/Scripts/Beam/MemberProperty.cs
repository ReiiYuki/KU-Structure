using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberProperty : MonoBehaviour {
    public float length;
    public int number,type;

    float[] E = { 1 };
    float[] I = { 1 };

    public float GetI()
    {
        return I[type];
    }

    public float GetE()
    {
        return E[type];
    }
}
