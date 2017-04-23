using System.Collections;
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
        public bool parabola;
        public Point(float x, float y,bool parabola)
        {
            this.x = x;
            this.y = y;
            this.parabola = parabola;
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
                float val = q[i];
                if (node.GetComponent<NodeProperty>().pointLoad)
                    val += node.GetComponent<NodeProperty>().pointLoad.load;
                GameObject reactionPointLoad = Instantiate(pointLoadPrefab, node.transform.position + new Vector3(0, 1), Quaternion.identity);
                val = (float)System.Math.Round(val, 2);
                if (val > 0)
                    reactionPointLoad.GetComponent<PointLoadProperty>().ForceInverse();
                else
                    reactionPointLoad.GetComponent<PointLoadProperty>().ABitInverse();
                reactionPointLoad.GetComponent<SpriteRenderer>().color = new Color(192 / 255f, 202 / 255f, 51 / 255f);
                reactionPointLoad.GetComponentInChildren<TextMesh>().text = val + " kg.";
                reactionPointLoad.GetComponentInChildren<TextMesh>().color = new Color(192 / 255f, 202 / 255f, 51 / 255f);
                reactionPointLoad.transform.SetParent(transform.GetChild(0));
                if (val == 0)
                    DestroyObject(reactionPointLoad);
            }
            if (System.Math.Round(q[i + 1],2) != 0)
            {
                GameObject reactionMomentum = Instantiate(momentumPrefab, node.transform.position - new Vector3(0, 0.75f, 0f), Quaternion.identity);
                reactionMomentum.GetComponent<MomentumProperty>().momentum = q[i+1];
                reactionMomentum.GetComponentInChildren<TextMesh>().text = System.Math.Round(-1*q[i+1],2) + " kg.m";
                reactionMomentum.GetComponentInChildren<TextMesh>().color = new Color(192 / 255f, 202 / 255f, 51 / 255f);
                reactionMomentum.GetComponentInChildren<TextMesh>().transform.position += new Vector3(0, 0.5f);
                reactionMomentum.GetComponentInChildren<SpriteRenderer>().color = new Color(192 / 255f, 202 / 255f, 51 / 255f);
                reactionMomentum.GetComponent<MomentumProperty>().UpdateDirection();
                reactionMomentum.transform.SetParent(transform.GetChild(0));
            }
        }
    }

    void DrawLoadDiagram()
    {
        originL = Instantiate(originPrefabs, new Vector3(0, -6, 0), Quaternion.identity);
        originL.transform.SetParent(transform);
        LineRenderer lineL = originL.GetComponent<LineRenderer>();
        lineL.startColor = new Color(128 / 255f, 222 / 255f, 234 / 255f);
        lineL.endColor = new Color(128 / 255f, 222 / 255f, 234 / 255f);
        originL.transform.SetParent(transform.GetChild(1));

        float val = 0;
        float x = 0;
        bool isStart = true;
        loadMem = new List<float>();
        List<Point> point = new List<Point>();
        foreach (GameObject member in collector.members)
        {
            MemberProperty property = member.GetComponent<MemberProperty>();
            if (isStart)
            {
                int node1Index = property.node1.number;
                float totalLoad = sfd.val[node1Index];
                if (collector.nodes[node1Index].GetComponent<NodeProperty>().pointLoad)
                    totalLoad += collector.nodes[node1Index].GetComponent<NodeProperty>().pointLoad.load;
                val += totalLoad;
                loadMem.Add(val);
                point.Add(new Point(x, val,false));
                isStart = false;
            }
            if (property.uniformLoad)
            {
                int node2Index = property.node2.number;
                val -= property.uniformLoad.load * property.length;
                x += property.length;
                loadMem.Add(val);
                point.Add(new Point(x, val, false));
                float totalLoad = sfd.val[node2Index];
                if (collector.nodes[node2Index].GetComponent<NodeProperty>().pointLoad)
                    totalLoad -= collector.nodes[node2Index].GetComponent<NodeProperty>().pointLoad.load;
                val += totalLoad;
                loadMem.Add(val);
                point.Add(new Point(x, val, false));
            }
            else
            {
                x += property.length;
                point.Add(new Point(x, val, false));
                int node2Index = property.node2.number;
                float totalLoad = sfd.val[node2Index];
                if (collector.nodes[node2Index].GetComponent<NodeProperty>().pointLoad)
                    totalLoad -= collector.nodes[node2Index].GetComponent<NodeProperty>().pointLoad.load;
                val += totalLoad;
                loadMem.Add(val);
                point.Add(new Point(x, val, false));
            }
        }

        foreach (Point p in point) Debug.Log("("+p.x+","+p.y+")");

        float currentX = 0;
        float currentY = 0;
        float max = Max(point);
        Debug.Log("max = " + max);
        foreach (Point p in point)
        {
            LineRenderer line = Instantiate(originPrefabs, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
            line.startColor = new Color(192 / 255f, 202 / 255f, 51 / 255f);
            line.endColor = new Color(192 / 255f, 202 / 255f, 51 / 255f);
            line.SetPositions(new Vector3[]
            {
                    new Vector3(currentX,currentY/max*3-6),
                    new Vector3(p.x,p.y/max*3-6)
            });
            currentX = p.x;
            currentY = p.y;
            line.transform.SetParent(transform.GetChild(1));
            if (System.Math.Round(p.y, 2) !=0)
            {
                TextMesh text = Instantiate(textPrefab, new Vector3(currentX, p.y / max * 3 - 5.8f), Quaternion.identity).GetComponent<TextMesh>();
                text.color = new Color(192 / 255f, 202 / 255f, 51 / 255f);
                text.text = System.Math.Round(p.y, 2) + "";
                text.characterSize = 0.2f;
                if (System.Math.Round(p.y, 2) < 0)
                    text.transform.position -= new Vector3(0, 0.4f);
                text.transform.SetParent(line.transform);
            }
        }
        
        lineL.SetPositions(new Vector3[] { new Vector3(0, -6), new Vector3(currentX, -6) });

        TextMesh textStart = Instantiate(textPrefab, new Vector3(-1f, -6), Quaternion.identity).GetComponent<TextMesh>();
        textStart.color = new Color(192 / 255f, 202 / 255f, 51 / 255f);
        textStart.text = "SFD";
        textStart.transform.SetParent(lineL.transform);

        TextMesh textEnd = Instantiate(textPrefab, new Vector3(x+1f, -6), Quaternion.identity).GetComponent<TextMesh>();
        textEnd.color = new Color(192 / 255f, 202 / 255f, 51 / 255f);
        textEnd.text = "SFD";
        textEnd.transform.SetParent(lineL.transform);

        string loadMemStr = "Load mem = ";
        foreach (float l in loadMem)
            loadMemStr += l + " ";
        Debug.Log(loadMemStr);
    }

    float Max(List<Point> ps)
    {
        float max = 0;
        foreach (Point p in ps)
            if (Mathf.Abs(p.y) > max) max = Mathf.Abs(p.y);
        return max;
    }

    void DrawMomentDiagram()
    {
        originM = Instantiate(originPrefabs, new Vector3(0, -16, 0), Quaternion.identity);
        originM.transform.SetParent(transform);
        LineRenderer lineM = originM.GetComponent<LineRenderer>();
        lineM.startColor = new Color(144/255f, 202/255f, 249/255f);
        lineM.endColor = new Color(144/255f, 202/255f, 249/255f);
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
                    points.Add(new Point(x, y,false));
                }
                else
                {
                    Debug.Log("index = " + index);
                    float separatePoint = FindPoint(loadMem[index++], loadMem[index], property.leftMember.length);
                    Debug.Log("Separate Point = " + separatePoint);
                    float left = property.leftMember.length-separatePoint;
                    Debug.Log("Left = " + left);
                    y += loadMem[index-1]*separatePoint/2;
                    x += separatePoint;
                    Debug.Log("y1 = " + y);
                    points.Add(new Point(x, y,true));

                    y += loadMem[index]*left/2;
                    x += left;
                    Debug.Log("y2 = " + y);
                    points.Add(new Point(x, y,true));
                }
            }
            if (bmd.val[property.number] != 0)
            {
                y += bmd.val[property.number];
                points.Add(new Point(x, y,false));
            }
        }

        float max = FindMaxPoint(points);
        float currentX = 0;
        float currentY = 0;
        float offset = -16;
        float parabolaIndex = 0;
        float[] parabolaEquation = new float[3];
        foreach (Point point in points)
        {
            LineRenderer line = Instantiate(originPrefabs, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
            line.startColor = new Color(192 / 255f, 202 / 255f, 51 / 255f);
            line.endColor = new Color(192 / 255f, 202 / 255f, 51 / 255f);

            if (!point.parabola)
            {
                line.SetPositions(new Vector3[] {
                    new Vector3(currentX,currentY/max*3+offset),
                    new Vector3(point.x,point.y/max*3+offset)
                });
            }else
            {
                int currentIndex = points.IndexOf(point);
                if (parabolaIndex == 0) {
                    parabolaEquation = GenerateParabolaEquation(points[currentIndex - 1], points[currentIndex], points[currentIndex + 1]);
                }
                DrawParabola(parabolaEquation, points[currentIndex - 1], points[currentIndex], line,max,offset);
                parabolaIndex++;
                parabolaIndex %= 2;
            }

            if (System.Math.Round(point.y, 2) != 0)
            {
                TextMesh text = Instantiate(textPrefab, new Vector3(point.x, point.y / max * 3 - 15.8f), Quaternion.identity).GetComponent<TextMesh>();
                text.color = new Color(192 / 255f, 202 / 255f, 51 / 255f);
                if (System.Math.Round(point.y, 2) < 0)
                    text.transform.position -= new Vector3(0, 0.4f);
                text.text = System.Math.Round(point.y, 2) + "";
                text.characterSize = 0.2f;
                text.transform.SetParent(line.transform);
            }
            

            currentX = point.x;
            currentY = point.y;
            line.transform.SetParent(transform.GetChild(2));
        }
        lineM.SetPositions(new Vector3[] { new Vector3(0, -16), new Vector3(currentX, -16) });

        TextMesh textStart = Instantiate(textPrefab, new Vector3(-1f, -16), Quaternion.identity).GetComponent<TextMesh>();
        textStart.color = new Color(192 / 255f, 202 / 255f, 51 / 255f);
        textStart.text = "BMD";
        textStart.transform.SetParent(lineM.transform);

        TextMesh textEnd = Instantiate(textPrefab, new Vector3(x + 1f, -16), Quaternion.identity).GetComponent<TextMesh>();
        textEnd.color = new Color(192 / 255f, 202 / 255f, 51 / 255f);
        textEnd.text = "BMD";
        textEnd.transform.SetParent(lineM.transform);
    }

    float FindPoint(float p1,float p2,float length)
    {
        Debug.Log("p1 = " + p1 + " p2 = " + p2 + " l = " + length);
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

    float[] GenerateParabolaEquation(Point p1,Point p2,Point p3)
    {
        float x1 = p1.x;
        float x2 = p2.x;
        float x3 = p3.x;
        float y1 = p1.y;
        float y2 = p2.y;
        float y3 = p3.y;
        Debug.Log("x1 = " + x1 + " y1 = " + y1 + " x2 = " + x2 + " y2 = " + y2 + " x3 = " + x3 + " y3 = " + y3);
        float a = (x3 * (y2 - y1) + x2 * (y1 - y3) + x1 * (y3 - y2)) / ((x1 - x2) * (x1 - x3) * (x2 - x3));
        float b = (x1 * x1 * (y2 - y3) + x3 * x3 * (y1 - y2) + x2 * x2 * (y3 - y1)) / ((x1 - x2) * (x1 - x3) * (x2 - x3));
        float c = (x2 * x2 * (x3 * y1 - x1 * y3) + x2 * (x1 * x1 * y3 - x3 * x3 * y1) + x1 * x3 * (x3 - x1) * y2) / ((x1 - x2) * (x1 - x3) * (x2 - x3));
        Debug.Log("a = " + a + " b = " + b + " c = " + c);
        return new float[]{ a,b,c };
    }

    void DrawParabola(float[] eq,Point point1,Point point2,LineRenderer line,float max,float offset)
    {

        float x = (float)System.Math.Round(point1.x,4);
        float y = (float)System.Math.Round(point1.y, 4);
        float targetX = (float)System.Math.Round(point2.x, 4);
        float targetY = (float)System.Math.Round(point2.y, 4);
        Debug.Log("a = " + eq[0] + "b = " + eq[1] + "c= " + eq[2]);
        List<Vector3> position = new List<Vector3>();
        while (x <= targetX )
        {
            float a = eq[0] * x * x;
            float b = eq[1] * x;
            float c = eq[2];
            y = a + b + c;
            position.Add( new Vector3(x, y/max*3+offset));
            x += 0.0001f;
        }
        line.SetVertexCount(position.Count);
        line.SetPositions(position.ToArray());
    }

    public void ResetGraphGenerator()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                DestroyObject(transform.GetChild(i).GetChild(j).gameObject);
            }
        }
    }
}
