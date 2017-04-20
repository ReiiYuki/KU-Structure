﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamGraphGenerator : MonoBehaviour {

    public GameObject originPrefabs,pointLoadPrefab,momentumPrefab,textPrefab;
    BeamAnalyzer.IndexArray sfd, bmd;
    BeamCollector collector;
    GameObject originL,originM;
    float[] q;
    List<float> loadMem;
    public struct Point
    {
        public float x, y;
        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    void Start()
    {
        collector = GameObject.FindObjectOfType<BeamCollector>();
    }

	public void GenerateGraph(BeamAnalyzer.IndexArray sfd,BeamAnalyzer.IndexArray bmd,float[] q) 
    {
        this.sfd = sfd;
        this.bmd = bmd;
        this.q = q;

        DrawForce();
        DrawLoadDiagram();
        DrawMomentDiagram();
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
                if (val == 0)
                    DestroyObject(reactionPointLoad);
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
        lineL.SetPositions(new Vector3[] { new Vector3(int.MinValue, -5), new Vector3(int.MaxValue, -5) });
        originL.transform.SetParent(transform.GetChild(1));

        float max = Max(sfd.val);
        float val = 0;
        float x = 0;
        bool isStart = true;
        loadMem = new List<float>();
        foreach (GameObject member in collector.members)
        {
            MemberProperty property = member.GetComponent<MemberProperty>();
            if (isStart)
            {
                int node1Index = property.node1.number;
                LineRenderer line1 = Instantiate(originPrefabs, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
                float totalLoad = sfd.val[node1Index];
                if (collector.nodes[node1Index].GetComponent<NodeProperty>().pointLoad)
                    totalLoad += collector.nodes[node1Index].GetComponent<NodeProperty>().pointLoad.load;
                line1.startColor = new Color(0 / 255f, 188 / 255f, 212 / 255f);
                line1.endColor = new Color(0 / 255f, 188 / 255f, 212 / 255f);
                line1.SetPositions(new Vector3[]
                {
                    new Vector3(x,val/max*3-5),
                    new Vector3(x,(val+totalLoad)/max*3-5)
                });
                val += totalLoad;
                loadMem.Add(val);
                line1.transform.SetParent(transform.GetChild(1));

                TextMesh text = Instantiate(textPrefab, new Vector3(x, val / max * 3 - 4.8f), Quaternion.identity).GetComponent<TextMesh>();
                text.color = new Color(0 / 255f, 188 / 255f, 212 / 255f);
                text.text = System.Math.Round(val, 4) + "";
                text.characterSize = 0.2f;
                if (System.Math.Round(val, 4) < 0)
                    text.transform.position -= new Vector3(0, 0.4f);
                text.transform.SetParent(line1.transform);

                isStart = false;
            }
            
            if (!property.uniformLoad)
            {
                LineRenderer line2 = Instantiate(originPrefabs, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
                line2.startColor = new Color(0 / 255f, 188 / 255f, 212 / 255f);
                line2.endColor = new Color(0 / 255f, 188 / 255f, 212 / 255f);
                line2.SetPositions(new Vector3[]
                {
                    new Vector3(x,val/max*3-5),
                    new Vector3(x+property.length,val/max*3-5)
                });
                x += property.length;
                line2.transform.SetParent(transform.GetChild(1));

                int node2Index = property.node2.number;
                LineRenderer line3 = Instantiate(originPrefabs, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
                float totalLoad = sfd.val[node2Index];
                if (collector.nodes[node2Index].GetComponent<NodeProperty>().pointLoad)
                    totalLoad -= collector.nodes[node2Index].GetComponent<NodeProperty>().pointLoad.load;
                line3.startColor = new Color(0 / 255f, 188 / 255f, 212 / 255f);
                line3.endColor = new Color(0 / 255f, 188 / 255f, 212 / 255f);
                line3.SetPositions(new Vector3[]
                {
                    new Vector3(x,val/max*3-5),
                    new Vector3(x,(val+totalLoad)/max*3-5)
                });
                val += totalLoad;
                loadMem.Add(val);
                line3.transform.SetParent(transform.GetChild(1));

                TextMesh text = Instantiate(textPrefab, new Vector3(x, val / max * 3 - 4.8f), Quaternion.identity).GetComponent<TextMesh>();
                if (System.Math.Round(val, 4) < 0)
                    text.transform.position -= new Vector3(0, 0.4f);
                text.color = new Color(0 / 255f, 188 / 255f, 212 / 255f);
                text.text = System.Math.Round(val, 4) + "";
                text.characterSize = 0.2f;
                text.transform.SetParent(line3.transform);
            }
            else
            {
                int node2Index = property.node2.number;
                float totalLoad = sfd.val[node2Index];
                if (collector.nodes[node2Index].GetComponent<NodeProperty>().pointLoad)
                    totalLoad -= collector.nodes[node2Index].GetComponent<NodeProperty>().pointLoad.load;
                totalLoad -= property.uniformLoad.load * property.length;
                LineRenderer line2 = Instantiate(originPrefabs, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
                line2.startColor = new Color(0 / 255f, 188 / 255f, 212 / 255f);
                line2.endColor = new Color(0 / 255f, 188 / 255f, 212 / 255f);
                line2.SetPositions(new Vector3[]
                {
                    new Vector3(x,val/max*3-5),
                    new Vector3(x+property.length,(val+totalLoad)/max*3-5)
                });
                val += totalLoad;
                loadMem.Add(val);
                x += property.length;
                line2.transform.SetParent(transform.GetChild(1));

                TextMesh text = Instantiate(textPrefab, new Vector3(x, val / max * 3 - 4.8f), Quaternion.identity).GetComponent<TextMesh>();
                text.color = new Color(0 / 255f, 188 / 255f, 212 / 255f);
                if (System.Math.Round(val, 4) < 0)
                    text.transform.position -= new Vector3(0, 0.4f);
                text.text = System.Math.Round(val,4) + "";
                text.characterSize = 0.2f;
                text.transform.SetParent(line2.transform);
            }
        }
        string loadMemStr = "Load mem = ";
        foreach (float l in loadMem)
            loadMemStr += l + " ";
        Debug.Log(loadMemStr);
    }

    float Max(float[] arr)
    {
        float max = int.MinValue;
        foreach (float e in arr)
            if (e > max) max = e;
        return max;
    }

    void DrawMomentDiagram()
    {
        originM = Instantiate(originPrefabs, new Vector3(0, -10, 0), Quaternion.identity);
        originM.transform.SetParent(transform);
        LineRenderer lineM = originM.GetComponent<LineRenderer>();
        lineM.startColor = new Color(144/255f, 202/255f, 249/255f);
        lineM.endColor = new Color(144/255f, 202/255f, 249/255f);
        lineM.SetPositions(new Vector3[] { new Vector3(int.MinValue, -10), new Vector3(int.MaxValue, -10) });
        lineM.transform.SetParent(transform.GetChild(2));

        List<Point> points = new List<Point>();
        float x = 0;
        float y = 0;
        int index = 0;
        foreach (GameObject node in collector.nodes)
        {
            NodeProperty property = node.GetComponent<NodeProperty>();
            if (property.leftMember)
            {
                if (!property.leftMember.uniformLoad)
                {
                    y += (property.leftMember.length) * loadMem[index++];
                    x += property.leftMember.length;
                    points.Add(new Point(x, y));
                }
                else
                {
                    float separatePoint = FindPoint(loadMem[index++], loadMem[index], property.leftMember.length);
                    float left = property.leftMember.length-separatePoint;
                    y += loadMem[index-1]*separatePoint/2;
                    x += separatePoint;
                    points.Add(new Point(x, y));

                    y += loadMem[index]*left/2;
                    x += left;
                    points.Add(new Point(x, y));
                }
            }
            if (bmd.val[property.number] != 0)
            {
                y += bmd.val[property.number];
                points.Add(new Point(x, y));
            }
        }

        float max = FindMaxPoint(points);
        float currentX = 0;
        float currentY = 0;
        float offset = -10;
        foreach (Point point in points)
        {
            LineRenderer line = Instantiate(originPrefabs, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
            line.startColor = new Color(33/255f, 150/255f, 243/255f);
            line.endColor = new Color(33 / 255f, 150 / 255f, 243 / 255f);
            line.SetPositions(new Vector3[] {
                new Vector3(currentX,currentY/max*3+offset),
                new Vector3(point.x,point.y/max*3+offset)
            });
            currentX = point.x;
            currentY = point.y;
            line.transform.SetParent(transform.GetChild(2));
        }
    }

    float FindPoint(float p1,float p2,float length)
    {
        return p1 * length / (p1 - p2);
    }

    float FindMaxPoint(List<Point> points)
    {
        float max = int.MinValue;
        foreach (Point point in points)
            if (point.y > max)
                max = point.y;
        return max;
    }
}
