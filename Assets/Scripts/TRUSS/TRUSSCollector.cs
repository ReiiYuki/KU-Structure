﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRUSSCollector : MonoBehaviour {

    public GameObject memberPrefab, textPrefab, nodePrefab, pointLoadPrefabX, pointLoadPrefabY, momentumPrefab, uniformLoadPrefab;
    public List<GameObject> nodes, members, pointLoads, forces, innerForces;
    public GameObject[] supportPrefabs;
    public bool visibleInputForce = true, visibleOuterForce = true, visibleInnerForce = true;
    public List<GameObject> history;
    // Use this for initialization
    void Start() {
        this.nodes = new List<GameObject>();
        this.members = new List<GameObject>();
        this.pointLoads = new List<GameObject>();
        this.forces = new List<GameObject>();
        this.innerForces = new List<GameObject>();
        this.history = new List<GameObject>();
    }

    // Update is called once per frame
    void Update() {

    }
    public void helpper()
    {
        // one
        //AddNode(0, 0);
        //AddNode(12, 0);
        //AddNode(24, 0);
        //AddNode(12, 16);
        //AddMember(0, 3, 1);
        //AddMember(1, 3, 0);
        //AddMember(2, 3, 1);
        //AddSupport(1, 0);
        //AddSupport(1, 1);
        //AddSupport(1, 2);
        //AddPointLoad(3, 150, -300);

        // 2
        AddNode(0, 0);
        AddNode(4, 8);
        AddNode(4, 4);
        AddNode(8, 0);
        AddMember(0, 1, 3);
        AddMember(0, 2, 3);
        AddMember(1, 2, 3);
        AddMember(2, 3, 3);
        AddMember(1, 3, 3);
        AddMember(0, 3, 3);
        AddSupport(1, 0);
        AddSupport(3, 3);
        AddPointLoad(1, 80, -120);

        //3
        //AddNode(0, 28.8f);
        //AddNode(19.2f, 28.8f);
        //AddNode(0, 0);
        //AddNode(19.2f, 14.4f);
        //AddMember(0, 1, 4);
        //AddMember(0, 3, 4);
        //AddMember(2, 1, 4);
        //AddMember(2, 3, 4);
        //AddMember(1, 3, 4);
        //AddSupport(1, 2);
        //AddSupport(1, 0);
        //AddSupport(3, 3);
        //AddPointLoad(1, 75, -150);
        //AddPointLoad(3, 75, 0);

        //AddNode(0, 0);
        //AddNode(0, 0);
        //////AddPointLoad(0, 100, 100);
        //////AddPointLoad(0, -100, -100);
        //////AddForce(0, -100, -1000);
        //AddForce(0, 154, 354);
        //AddForce(0, -1154, -1354);
        //AddForce(1, -154, -354);
        //AddForce(1, 1154, 1354);

        //for(int i=0;i<5;i++)
        //{
        //    AddNode(i*2, 0);
        //    AddSupport(i, i);
        //}


    }
    public void ResetAll()
    {
        foreach (GameObject g in history)
        {
            DestroyImmediate(g, true);
        }
        members.Clear();
        nodes.Clear();
        history.Clear();
        pointLoads.Clear();
        innerForces.Clear();

        forces.Clear();
    }
    public void Undo()
    {
        if (history.Count == 0) return;
        GameObject temp = history[history.Count - 1];
        if (temp.GetComponent<TrussPointLoadProperty>())
        {
            if (pointLoads.Contains(temp))
            {
                foreach (GameObject node in nodes)
                {
                    if (node.GetComponent<TrussNodeProperty>().number == temp.GetComponent<TrussPointLoadProperty>().node)
                    {
                        if (temp.GetComponent<TrussPointLoadProperty>().axis == 'x')
                        {
                            node.GetComponent<TrussNodeProperty>().pointLoadX = null;
                        }
                        if (temp.GetComponent<TrussPointLoadProperty>().axis == 'y')
                        {
                            node.GetComponent<TrussNodeProperty>().pointLoadY = null;
                        }
                    }

                }

                pointLoads.Remove(temp);
            }
            if (forces.Contains(temp))
            {
                forces.Remove(temp);
            }
            if (innerForces.Contains(temp))
            {
                innerForces.Remove(temp);
            }
        }

        else if (temp.GetComponent<TrussMemberProperty>())
        {
            foreach (GameObject node in nodes)
            {
                if (temp.GetComponent<TrussMemberProperty>().node1.number == node.GetComponent<TrussNodeProperty>().number)
                    node.GetComponent<TrussNodeProperty>().members.Remove(temp.GetComponent<TrussMemberProperty>());
                if (temp.GetComponent<TrussMemberProperty>().node2.number == node.GetComponent<TrussNodeProperty>().number)
                    node.GetComponent<TrussNodeProperty>().members.Remove(temp.GetComponent<TrussMemberProperty>());
            }
            members.Remove(temp);
        }
        else if (temp.GetComponent<TrussNodeProperty>())
        {
            //foreach(TrussMemberProperty m in temp.GetComponent<TrussNodeProperty>().members)
            //{
            //    if(temp.GetComponent<TrussNodeProperty>().Equals(m.node1))
            //    {
            //        m.node1 = null;
            //    }
            //    if (temp.GetComponent<TrussNodeProperty>().Equals(m.node2))
            //    {
            //        m.node1 = null;
            //    }
            //}
            //temp.GetComponent<TrussNodeProperty>().support.node = null;
            //foreach(GameObject pointLoad in pointLoads)
            //{
            //    if(temp.GetComponent<TrussNodeProperty>().pointLoadX.Equals(pointLoad.GetComponent<TrussPointLoadProperty>()))
            //    {
            //        pointLoads.Remove(pointLoad);
            //        DestroyImmediate(pointLoad, true);
            //    }
            //    if (temp.GetComponent<TrussNodeProperty>().pointLoadY.Equals(pointLoad.GetComponent<TrussPointLoadProperty>()))
            //    {
            //        pointLoads.Remove(pointLoad);
            //        DestroyImmediate(pointLoad, true);
            //    }
            //}
            //foreach (GameObject force in forces)
            //{
            //    if (force.GetComponent<TrussPointLoadProperty>().Equals(temp.GetComponent<TrussNodeProperty>().forceX))
            //    {
            //        forces.Remove(force);
            //        DestroyImmediate(force, true);
            //    }
            //    if (force.GetComponent<TrussPointLoadProperty>().Equals(temp.GetComponent<TrussNodeProperty>().forceY))
            //    {
            //        forces.Remove(force);
            //        DestroyImmediate(force, true);
            //    }
            //}
            nodes.Remove(temp);
        }
        history.Remove(temp);
        DestroyImmediate(temp, true);
    }
    public void AddNode(float x, float y)
    {
        Debug.Log("Add Node { x:" + x + " y : " + y + " }");
        // create a new node
        GameObject node = Instantiate(nodePrefab, new Vector3(x, y), Quaternion.identity);
        Camera.main.transform.position = new Vector3(x, y, Camera.main.transform.position.z);
        // init node vriable
        node.GetComponent<TrussNodeProperty>().x = x;
        node.GetComponent<TrussNodeProperty>().y = y;
        node.GetComponent<TrussNodeProperty>().dx = 1;
        node.GetComponent<TrussNodeProperty>().dy = 1;
        Debug.Log(node.GetComponent<TrussNodeProperty>().dx);
        Debug.Log(node.GetComponent<TrussNodeProperty>().dy);
        node.GetComponent<TrussNodeProperty>().number = nodes.Count;
        node.GetComponentInChildren<TextMesh>().text = nodes.Count + "";
        nodes.Add(node);
        history.Add(node);
    }

    public void AddMember(int node1, int node2, int property)
    {
        // get all the position of the node
        float node1X = nodes[node1].transform.position.x;
        float node1Y = nodes[node1].transform.position.y;
        float node2X = nodes[node2].transform.position.x;
        float node2Y = nodes[node2].transform.position.y;
        Debug.Log("Add Member { node1 : " + node1 + " node2 : " + node2 + " property : " + property + " }");

        // create a new member
        GameObject member = Instantiate(memberPrefab, Vector3.zero, Quaternion.identity);
        // draw line
        LineRenderer line = member.GetComponent<LineRenderer>();
        line.startColor = GetColor(members.Count);
        line.endColor = GetColor(members.Count);
        line.SetPositions(new Vector3[] { new Vector3(node1X, node1Y), new Vector3(node2X, node2Y) });
        Camera.main.transform.position = new Vector3((node1X + node2X) / 2, (node1Y + node2Y) / 2, Camera.main.transform.position.z);

        // init member variable
        TrussMemberProperty memberProperty = member.GetComponent<TrussMemberProperty>();
        memberProperty.type = property;
        memberProperty.number = members.Count;
        memberProperty.node1 = nodes[node1].GetComponent<TrussNodeProperty>();
        memberProperty.node2 = nodes[node2].GetComponent<TrussNodeProperty>();

        // add member to node
        nodes[node1].GetComponent<TrussNodeProperty>().members.Add(memberProperty);
        nodes[node2].GetComponent<TrussNodeProperty>().members.Add(memberProperty);

        // draw the length of the member
        float slope = (node1Y - node2Y) / (node1X - node2X);
        float b = node2Y - node2X * slope;
        float invSlope = -1 / slope;
        float invB = (node2Y + node1Y) / 2 - (node2X + node1X) / 2 * invSlope;
        float newX = ((node1Y + node2Y) / 2f + 1.5f - invB) / invSlope;
        float newY = ((node1X + node2X) / 2f + 1.5f) * invSlope + invB;
        if (slope > 0)
            newY = ((node1X + node2X) / 2f - 1.5f) * invSlope + invB;
        if (invSlope == 0)
        {
            newX = (node1X + node2X) / 2f + 2f;
            newY = (node1Y + node2Y) / 2f;
        }
        if (slope == 0)
        {
            newX = (node1X + node2X) / 2f;
            newY = (node1Y + node2Y) / 2f + 2f;
        }
        Debug.Log(invSlope);
        Debug.Log(newX);
        Debug.Log(newY);
        GameObject lengthText = Instantiate(textPrefab, new Vector3(newX, newY), Quaternion.identity);
        lengthText.GetComponent<TextMesh>().text = memberProperty.lenght() + " m.";
        lengthText.GetComponent<TextMesh>().fontSize = 16;
        float result = (float)(Math.Atan2((node1Y - node2Y), (node1X - node2X)) * 180 / Math.PI);
        lengthText.GetComponent<TextMesh>().transform.Rotate(new Vector3(-180, -180, result));

        lengthText.transform.SetParent(member.transform);

        // draw a number of the member
        GameObject numberText = Instantiate(textPrefab, new Vector3((node1X + node2X) / 2f, (node1Y + node2Y) / 2f), Quaternion.identity);
        numberText.GetComponent<TextMesh>().text = members.Count + "";
        numberText.GetComponent<TextMesh>().color = Color.white;
        numberText.transform.SetParent(member.transform);



        member.transform.SetParent(transform);
        members.Add(member);
        history.Add(member);
    }

    public void AddSupport(int type, int node)
    {
        Debug.Log("Add Support { type : " + type + " node : " + node + " }");

        GameObject selectedNode = nodes[node];
        GameObject support;
        if (type == 0)
        {
            support = Instantiate(supportPrefabs[type], selectedNode.transform.position - new Vector3(-0.0f, 0), Quaternion.identity);
            support.transform.Rotate(new Vector3(0, 0, -90f));
        }
        else if (type == 1)
        {
            support = Instantiate(supportPrefabs[type], selectedNode.transform.position - new Vector3(0, 0.55f), Quaternion.identity);
        }
        else if (type == 2)
        {
            support = Instantiate(supportPrefabs[type], selectedNode.transform.position - new Vector3(0.6f, 0), Quaternion.identity);
            support.transform.Rotate(new Vector3(1, 1, -90));
        }
        else if (type == 3)
        {
            support = Instantiate(supportPrefabs[type], selectedNode.transform.position - new Vector3(0, 0.55f), Quaternion.identity);
        }
        else
        {
            support = Instantiate(supportPrefabs[type], selectedNode.transform.position - new Vector3(0.6f, 0), Quaternion.identity);
            support.transform.Rotate(new Vector3(1, 1, -90));
        }
        Camera.main.transform.position = new Vector3(selectedNode.transform.position.x, selectedNode.transform.position.y, Camera.main.transform.position.z);
        // init support variable
        support.GetComponent<TrussSupportProperty>().node = nodes[node].GetComponent<TrussNodeProperty>();

        // add degree of freedom to node
        if (type == 0 || type == 1 || type == 2)
        {
            nodes[node].GetComponent<TrussNodeProperty>().dx = 0;
            nodes[node].GetComponent<TrussNodeProperty>().dy = 0;
        }
        else if (type == 3)
        {
            nodes[node].GetComponent<TrussNodeProperty>().dy = 0;
        }
        else
        {
            nodes[node].GetComponent<TrussNodeProperty>().dx = 0;
        }
        // add support to node
        nodes[node].GetComponent<TrussNodeProperty>().support = support.GetComponent<TrussSupportProperty>();

        support.transform.SetParent(selectedNode.transform);
        history.Add(support);
    }

    public void AddPointLoad(int node, float loadX, float loadY)
    {
        Debug.Log("Add Point Load { node : " + node + " loadX : " + loadX + " loadY : " + loadY + " }");
        GameObject selectNode = nodes[node];
        Camera.main.transform.position = new Vector3(selectNode.transform.position.x, selectNode.transform.position.y, Camera.main.transform.position.z);

        // add load X
        if (loadX != 0) {
            GameObject pointLoadX = Instantiate(pointLoadPrefabX, new Vector3((nodes[node].transform.position.x + 1.25f), (nodes[node].transform.position.y)), Quaternion.identity);
            pointLoadX.transform.Rotate(new Vector3(0, 0, -90));
            pointLoadX.GetComponentInChildren<TextMesh>().text = loadX + " N.";
            pointLoadX.GetComponent<TrussPointLoadProperty>().load = loadX;
            pointLoadX.GetComponent<TrussPointLoadProperty>().axis = 'x';
            pointLoadX.GetComponent<TrussPointLoadProperty>().node = node;
            selectNode.GetComponent<TrussNodeProperty>().pointLoadX = pointLoadX.GetComponent<TrussPointLoadProperty>();
            pointLoadX.GetComponent<TrussPointLoadProperty>().Inverse();

            pointLoadX.transform.SetParent(selectNode.transform);
            history.Add(pointLoadX);
            pointLoads.Add(pointLoadX);
        }

        // add Load Y
        if (loadY != 0) {
            GameObject pointLoadY = Instantiate(pointLoadPrefabY, new Vector3((nodes[node].transform.position.x), (nodes[node].transform.position.y + 1.25f)), Quaternion.identity);

            pointLoadY.GetComponentInChildren<TextMesh>().text = loadY + " N.";

            pointLoadY.GetComponent<TrussPointLoadProperty>().load = loadY;
            pointLoadY.GetComponent<TrussPointLoadProperty>().axis = 'y';
            pointLoadY.GetComponent<TrussPointLoadProperty>().node = node;
            selectNode.GetComponent<TrussNodeProperty>().pointLoadY = pointLoadY.GetComponent<TrussPointLoadProperty>();

            pointLoadY.GetComponent<TrussPointLoadProperty>().Inverse();

            pointLoadY.transform.SetParent(selectNode.transform);
            history.Add(pointLoadY);
            pointLoads.Add(pointLoadY);
        }
    }

    public void AddQ(TrussMemberProperty m, int node, float q, bool invert)
    {

        float slope = (m.node1.y - m.node2.y) / (m.node1.x - m.node2.x);


        GameObject pointLoadX = Instantiate(pointLoadPrefabX, new Vector3(nodes[node].GetComponent<TrussNodeProperty>().x, nodes[node].GetComponent<TrussNodeProperty>().y, 0), Quaternion.identity);
        Camera.main.transform.position = new Vector3(nodes[node].GetComponent<TrussNodeProperty>().transform.position.x, nodes[node].GetComponent<TrussNodeProperty>().transform.position.y, Camera.main.transform.position.z);

        pointLoadX.GetComponent<SpriteRenderer>().color = new Color(33 / 255f, 150 / 255f, 243 / 255f);
        pointLoadX.GetComponentInChildren<TextMesh>().color = new Color(33 / 255f, 150 / 255f, 243 / 255f);
        float result = (float)System.Math.Atan(slope) * 180 / (float)Math.PI;
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        Debug.Log("Member: " + m.number + ", Node: " + node + ", q= " + q + ", slope: " + slope + ", atan: " + result + " " + (float)Math.Atan(slope) + " ,degree: " + result + ", rad=" + 180 / (float)Math.PI);
        if (m.node1.GetComponent<TrussNodeProperty>().Equals(nodes[node].GetComponent<TrussNodeProperty>()))
        {
            if (m.node2.GetComponent<TrussNodeProperty>().x < nodes[node].GetComponent<TrussNodeProperty>().x)
            {
                //result += 180;
                pointLoadX.transform.position += new Vector3(1, 0);
            }
            if (m.node2.GetComponent<TrussNodeProperty>().x > nodes[node].GetComponent<TrussNodeProperty>().x)
            {
                //result += 180;
                pointLoadX.transform.position += new Vector3(1, 0);
            }
            if (m.node2.GetComponent<TrussNodeProperty>().y < nodes[node].GetComponent<TrussNodeProperty>().y)
            {
                if (float.IsPositiveInfinity(slope))
                {
                    result += 180;
                    pointLoadX.transform.position += new Vector3(0, -1);
                }
                else
                {
                    pointLoadX.transform.position += new Vector3(0, slope);
                }
            }
            if (m.node2.GetComponent<TrussNodeProperty>().y > nodes[node].GetComponent<TrussNodeProperty>().y)
            {
                if (!float.IsPositiveInfinity(slope))
                {
                    pointLoadX.transform.position += new Vector3(0, slope);
                }
            }

        }
        if (m.node2.GetComponent<TrussNodeProperty>().Equals(nodes[node].GetComponent<TrussNodeProperty>()))
        {
            if (m.node1.GetComponent<TrussNodeProperty>().x < nodes[node].GetComponent<TrussNodeProperty>().x)
            {
                //n1-------n2
                result += 180;
                pointLoadX.transform.position += new Vector3(-1, 0);
            }
            if (m.node1.GetComponent<TrussNodeProperty>().x > nodes[node].GetComponent<TrussNodeProperty>().x)
            {
                //n1-------n2
                result += 180;
                pointLoadX.transform.position += new Vector3(1, 0);
            }
            if (m.node1.GetComponent<TrussNodeProperty>().y < nodes[node].GetComponent<TrussNodeProperty>().y)
            {
                pointLoadX.transform.position += new Vector3(0, -slope);
            }
            if (!float.IsPositiveInfinity(slope) && m.node1.GetComponent<TrussNodeProperty>().y > nodes[node].GetComponent<TrussNodeProperty>().y)
            {
                pointLoadX.transform.position += new Vector3(0, -slope);
            }
            if (float.IsPositiveInfinity(slope) && m.node1.GetComponent<TrussNodeProperty>().x == nodes[node].GetComponent<TrussNodeProperty>().x)
            {
                pointLoadX.transform.position += new Vector3(0, 1);
            }

        }
        if (invert)
            result += 180;
        pointLoadX.transform.Rotate(new Vector3(1, 1, result + 90));
        pointLoadX.GetComponentInChildren<TextMesh>().text = q + " N.";
        pointLoadX.GetComponent<TrussPointLoadProperty>().text = pointLoadX.GetComponentInChildren<TextMesh>();
        pointLoadX.GetComponent<TrussPointLoadProperty>().load = q;
        pointLoadX.GetComponent<TrussPointLoadProperty>().axis = 'x';
        pointLoadX.GetComponent<TrussPointLoadProperty>().node = node;
        pointLoadX.GetComponent<TrussPointLoadProperty>().nodeG = nodes[node];
        //pointLoadX.transform.eulerAngles =new Vector3(slope,0,0);
        innerForces.Add(pointLoadX);
        history.Add(pointLoadX);
    }
    public void AddForce(int node, float loadX, float loadY)
    {
        Debug.Log("Add Point Load { node : " + node + " loadX : " + loadX + " loadY : " + loadY + " }");
        GameObject selectNode = nodes[node];
        Camera.main.transform.position = new Vector3(selectNode.transform.position.x, selectNode.transform.position.y, Camera.main.transform.position.z);

        // add load X
        if (loadX != 0 && selectNode.GetComponent<TrussNodeProperty>().pointLoadX == null)
        {
            if (selectNode.GetComponent<TrussNodeProperty>().forceX != null)
            {
                selectNode.GetComponent<TrussNodeProperty>().forceX.load += loadX;
                selectNode.GetComponent<TrussNodeProperty>().forceX.text.text = selectNode.GetComponent<TrussNodeProperty>().forceX.load + " N.";
                Debug.Log(selectNode.GetComponent<TrussNodeProperty>().forceX.text.text + "  " + selectNode.GetComponent<TrussNodeProperty>().forceX.axis);
                selectNode.GetComponent<TrussNodeProperty>().forceX.InverseForce();
            }
            else
            {
                GameObject pointLoadX = Instantiate(pointLoadPrefabX, new Vector3(0, 0, 0), Quaternion.identity);
                //Material newMaterial = new Material(Shader.Find("Specular"));
                //newMaterial.color = new Color(255, 182, 0, 1);
                //pointLoadX.GetComponent<MeshRenderer>().material = newMaterial;
                pointLoadX.GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 179 / 255f, 0 / 255f);
                pointLoadX.transform.Rotate(new Vector3(0, 0, -90));
                pointLoadX.GetComponentInChildren<TextMesh>().text = loadX + " N.";
                pointLoadX.GetComponentInChildren<TextMesh>().color = new Color(255 / 255f, 179 / 255f, 0 / 255f);
                pointLoadX.GetComponent<TrussPointLoadProperty>().text = pointLoadX.GetComponentInChildren<TextMesh>();
                pointLoadX.GetComponent<TrussPointLoadProperty>().load = loadX;
                pointLoadX.GetComponent<TrussPointLoadProperty>().axis = 'x';
                pointLoadX.GetComponent<TrussPointLoadProperty>().node = node;
                pointLoadX.GetComponent<TrussPointLoadProperty>().nodeG = nodes[node];
                selectNode.GetComponent<TrussNodeProperty>().forceX = pointLoadX.GetComponent<TrussPointLoadProperty>();
                pointLoadX.GetComponent<TrussPointLoadProperty>().InverseForce();

                pointLoadX.transform.SetParent(selectNode.transform);
                history.Add(pointLoadX);
                forces.Add(pointLoadX);
            }

        }

        // add Load Y
        if (loadY != 0 && selectNode.GetComponent<TrussNodeProperty>().pointLoadY == null)
        {
            if (selectNode.GetComponent<TrussNodeProperty>().forceY != null)
            {
                selectNode.GetComponent<TrussNodeProperty>().forceY.load += loadY;
                selectNode.GetComponent<TrussNodeProperty>().forceY.text.text = selectNode.GetComponent<TrussNodeProperty>().forceY.load + " N.";
                Debug.Log(selectNode.GetComponent<TrussNodeProperty>().forceY.text.text + "  " + selectNode.GetComponent<TrussNodeProperty>().forceY.axis);
                selectNode.GetComponent<TrussNodeProperty>().forceY.InverseForce();
            }
            else
            {
                GameObject pointLoadY = Instantiate(pointLoadPrefabY, new Vector3(0, 0, 0), Quaternion.identity);
                pointLoadY.GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 179 / 255f, 0 / 255f);
                pointLoadY.GetComponentInChildren<TextMesh>().text = loadY + " N.";
                pointLoadY.GetComponentInChildren<TextMesh>().color = new Color(255 / 255f, 179 / 255f, 0 / 255f);
                pointLoadY.GetComponent<TrussPointLoadProperty>().text = pointLoadY.GetComponentInChildren<TextMesh>();
                pointLoadY.GetComponent<TrussPointLoadProperty>().load = loadY;
                pointLoadY.GetComponent<TrussPointLoadProperty>().axis = 'y';
                pointLoadY.GetComponent<TrussPointLoadProperty>().node = node;
                pointLoadY.GetComponent<TrussPointLoadProperty>().nodeG = nodes[node];
                selectNode.GetComponent<TrussNodeProperty>().forceY = pointLoadY.GetComponent<TrussPointLoadProperty>();

                pointLoadY.GetComponent<TrussPointLoadProperty>().InverseForce();

                pointLoadY.transform.SetParent(selectNode.transform);
                history.Add(pointLoadY);
                forces.Add(pointLoadY);
            }

        }

    }
    public Color GetColor(int x)
    {
        if (x % 2 == 0) return new Color(169 / 255f, 169 / 255f, 169 / 255f);
        return new Color(112 / 255f, 128 / 255f, 144 / 255f);
    }
    public float slope(List<TrussMemberProperty> list)
    {
        float slope = 0;
        foreach (TrussMemberProperty m in list)
        {
            slope += (m.node1.y - m.node2.y) / (m.node1.x - m.node2.x);
        }

        return slope;
    }
    public void toggleInputForce()
    {
        if(visibleInputForce)
        {
            foreach (GameObject g in pointLoads)
                g.SetActive(false);
            visibleInputForce = false;
        }
        else
        {
            foreach (GameObject g in pointLoads)
                g.SetActive(true);
            visibleInputForce = true;

        }
    }
    public void toggleOuterForce()
    {
        if (visibleOuterForce)
        {
            foreach (GameObject g in forces)
                g.SetActive(false);
            visibleOuterForce = false;
        }
        else
        {
            foreach (GameObject g in forces)
                g.SetActive(true);
            visibleOuterForce = true;

        }
    }
    public void toggleInnerForce()
    {
        if (visibleInnerForce)
        {
            foreach (GameObject g in innerForces)
                g.SetActive(false);
            visibleInnerForce = false;
        }
        else
        {
            foreach (GameObject g in innerForces)
                g.SetActive(true);
            visibleInnerForce = true;

        }
    }

}
