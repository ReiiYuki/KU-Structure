using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentumProperty : MonoBehaviour {

    public float momentum;
    public int node;

    public void UpdateDirection()
    {
        if (momentum < 0) transform.localScale = new Vector2(0.1f, 0.1f);
    } 
}
