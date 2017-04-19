using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamGraphGenerator : MonoBehaviour {

    public GameObject originPrefabs,pointLoadPrefab,momentumPrefab;
    BeamAnalyzer.IndexArray sfd, bmd;
    BeamCollector collector;
    GameObject originL,originM;

    void Start()
    {
        collector = GameObject.FindObjectOfType<BeamCollector>();
    }

	public void GenerateGraph(BeamAnalyzer.IndexArray sfd,BeamAnalyzer.IndexArray bmd) 
    {
        this.sfd = sfd;
        this.bmd = bmd;

        InitOrigin();
        DrawForce();
    }

    void DrawForce()
    {
        for (int i = 0; i < sfd.val.Length; i++)
        {
            GameObject node = collector.nodes[sfd.index[i]/2];
            if (sfd.val[i] != 0)
            {
                GameObject reactionPointLoad = Instantiate(pointLoadPrefab, node.transform.position + new Vector3(0, 1), Quaternion.identity);
                Debug.Log(sfd.val[i]);
                if (sfd.val[i] >= 0)
                    reactionPointLoad.GetComponent<PointLoadProperty>().ForceInverse();
                reactionPointLoad.GetComponent<SpriteRenderer>().color = new Color(100 / 255f, 181 / 255f, 246 / 255f);
                reactionPointLoad.GetComponentInChildren<TextMesh>().text = sfd.val[i] + " N.";
                reactionPointLoad.GetComponentInChildren<TextMesh>().color = new Color(100 / 255f, 181 / 255f, 246 / 255f);
            }
        }
    }

    void InitOrigin()
    {
        originL = Instantiate(originPrefabs, new Vector3(0, -5, 0), Quaternion.identity);
        originL.transform.SetParent(transform);
        LineRenderer lineL = originL.GetComponent<LineRenderer>();
        lineL.SetPositions(new Vector3[] { new Vector3(-100, -5), new Vector3(100,-5) });

        originM = Instantiate(originPrefabs, new Vector3(0, -10, 0), Quaternion.identity);
        originM.transform.SetParent(transform);
        LineRenderer lineM = originM.GetComponent<LineRenderer>();
        lineM.SetPositions(new Vector3[] { new Vector3(-100, -8), new Vector3(100, -8) });
    }
}
