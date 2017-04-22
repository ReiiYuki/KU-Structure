using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrussNodeProperty : MonoBehaviour
{

    public int number;
    public float dx, dy, x,y;
    public TrussPointLoadProperty pointLoadX,pointLoadY;
    public TrussSupportProperty support;
    public List<TrussMemberProperty> members;

    void Start()
    {
        members = new List<TrussMemberProperty>();
    }

}
