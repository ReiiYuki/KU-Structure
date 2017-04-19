using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamGraphGenerator : MonoBehaviour {

    public GameObject originPrefabs;
    BeamAnalyzer.IndexArray sfd, bmd;
    GameObject originL,originM;

	public void GenerateGraph(BeamAnalyzer.IndexArray sfd,BeamAnalyzer.IndexArray bmd) 
    {
        this.sfd = sfd;
        this.bmd = bmd;

        InitOrigin();
    }

    public void InitOrigin()
    {
        originL = Instantiate(originPrefabs, new Vector3(0, -5, 0), Quaternion.identity);
        originL.transform.SetParent(transform);
        LineRenderer lineL = originL.GetComponent<LineRenderer>();
        lineL.SetPositions(new Vector3[] { new Vector3(-100, -5), new Vector3(100,-5) });

        originM = Instantiate(originPrefabs, new Vector3(0, -10, 0), Quaternion.identity);
        originM.transform.SetParent(transform);
        LineRenderer lineM = originL.GetComponent<LineRenderer>();
        lineM.SetPositions(new Vector3[] { new Vector3(-100, -8), new Vector3(100, -8) });
    }
}
