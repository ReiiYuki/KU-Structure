using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrussNodeProperty : MonoBehaviour
{

    public int number;
    public float dx = 1, dy = 1, x,y;
    public TrussPointLoadProperty pointLoadX,pointLoadY, forceX, forceY;
    public TrussSupportProperty support;
    public List<TrussMemberProperty> members;

    void Start()
    {
        members = new List<TrussMemberProperty>();
        
    }

}
