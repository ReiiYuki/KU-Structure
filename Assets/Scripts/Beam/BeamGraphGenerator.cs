using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamGraphGenerator : MonoBehaviour {

    public GameObject originPrefabs,pointLoadPrefab,momentumPrefab;
    BeamAnalyzer.IndexArray sfd, bmd;
    BeamCollector collector;
    GameObject originL,originM;
    float[] q;

    void Start()
    {
        collector = GameObject.FindObjectOfType<BeamCollector>();
    }

	public void GenerateGraph(BeamAnalyzer.IndexArray sfd,BeamAnalyzer.IndexArray bmd,float[] q) 
    {
        this.sfd = sfd;
        this.bmd = bmd;
        this.q = q;

        InitOrigin();
        DrawForce();
        DrawLoadDiagram();
    }

    void DrawForce()
    {
        for (int i = 0; i < q.Length; i += 2)
        {
            GameObject node = collector.nodes[i/2];
            if (q[i] != 0)
            {
                GameObject reactionPointLoad = Instantiate(pointLoadPrefab, node.transform.position + new Vector3(0, 1), Quaternion.identity);
                float val = q[i];
                if (node.GetComponent<NodeProperty>().pointLoad)
                    val += node.GetComponent<NodeProperty>().pointLoad.load;
                if (val > 0)
                    reactionPointLoad.GetComponent<PointLoadProperty>().ForceInverse();
                else
                    reactionPointLoad.GetComponent<PointLoadProperty>().ABitInverse();
                reactionPointLoad.GetComponent<SpriteRenderer>().color = new Color(100 / 255f, 181 / 255f, 246 / 255f);
                reactionPointLoad.GetComponentInChildren<TextMesh>().text = val + " N.";
                reactionPointLoad.GetComponentInChildren<TextMesh>().color = new Color(100 / 255f, 181 / 255f, 246 / 255f);
                reactionPointLoad.transform.SetParent(transform.GetChild(0));
            }
            if (q[i + 1] != 0)
            {
                GameObject reactionMomentum = Instantiate(momentumPrefab, node.transform.position - new Vector3(0, 0.75f, 0f), Quaternion.identity);
                reactionMomentum.GetComponent<MomentumProperty>().momentum = q[i+1];
                reactionMomentum.GetComponentInChildren<TextMesh>().text = (-1*q[i+1]) + " N.m";
                reactionMomentum.GetComponentInChildren<TextMesh>().color = new Color(100 / 255f, 181 / 255f, 246 / 255f);
                reactionMomentum.GetComponentInChildren<TextMesh>().transform.position += new Vector3(0, 0.5f);
                reactionMomentum.GetComponentInChildren<SpriteRenderer>().color = new Color(100 / 255f, 181 / 255f, 246 / 255f);
                reactionMomentum.GetComponent<MomentumProperty>().UpdateDirection();
                reactionMomentum.transform.SetParent(transform.GetChild(0));
            }
        }
    }

    void DrawLoadDiagram()
    {
        originL = Instantiate(originPrefabs, new Vector3(0, -5, 0), Quaternion.identity);
        originL.transform.SetParent(transform);
        LineRenderer lineL = originL.GetComponent<LineRenderer>();
        lineL.startColor = new Color(128 / 255f, 222 / 255f, 234 / 255f);
        lineL.endColor = new Color(128 / 255f, 222 / 255f, 234 / 255f);
        lineL.SetPositions(new Vector3[] { new Vector3(-100, -5), new Vector3(100, -5) });
        originL.transform.SetParent(transform.GetChild(1));


    }

    void InitOrigin()
    {
        

        originM = Instantiate(originPrefabs, new Vector3(0, -10, 0), Quaternion.identity);
        originM.transform.SetParent(transform);
        LineRenderer lineM = originM.GetComponent<LineRenderer>();
        lineM.SetPositions(new Vector3[] { new Vector3(-100, -8), new Vector3(100, -8) });
    }
}
