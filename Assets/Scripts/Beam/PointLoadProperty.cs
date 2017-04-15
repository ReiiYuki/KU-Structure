using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLoadProperty : MonoBehaviour {

    public float load;
    public int node;

    public void Inverse()
    {
        if (load < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
            transform.GetChild(0).localScale = new Vector3(transform.GetChild(0).localScale.x, transform.GetChild(0).localScale.y * -1, transform.GetChild(0).localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y - 2.75f);
        }
    }
}
