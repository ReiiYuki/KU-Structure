using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAnalyzer : MonoBehaviour {

    BeamCollector collector;

    float[] df;

    private void Start()
    {
        collector = GetComponent<BeamCollector>();
    }

    public void Analyze()
    {
        GenerateDegreeOfFreedom();
    }

    public void GenerateDegreeOfFreedom()
    {
        df = new float[collector.nodes.Count*2];
        int index = 0;
        foreach (GameObject node in collector.nodes)
        {
            df[index++] = node.GetComponent<NodeProperty>().dy;
            df[index++] = node.GetComponent<NodeProperty>().m;
        }
        Debug.Log(df);
    }

    public void ResetAnalyzer()
    {

    }
}
