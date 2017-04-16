﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAnalyzer : MonoBehaviour {

    BeamCollector collector;

    float[] df;
    List<IndexMatrix> k;
    IndexMatrix s;
    List<IndexArray> pi,qf;
    IndexArray pf,p;

    struct IndexArray
    {
        public List<int> index;
        public float[] val;
        public IndexArray(List<int> index,float[] val)
        {
            this.index = index;
            this.val = val;
        }
    }

    struct IndexMatrix
    {
        public List<int> index;
        public float[,] k_val;
        public IndexMatrix(List<int> index,float[,] k_val)
        {
            this.index = index;
            this.k_val = k_val;
        }
    }

    private void Start()
    {
        collector = GetComponent<BeamCollector>();
    }

    public void Analyze()
    {
        GenerateDegreeOfFreedom();
        GenerateAllK();
        GenerateS();
        GeneratePi();
        GenerateP();
        GenerateQF();
        GeneratePF();
    }

    void GenerateDegreeOfFreedom()
    {
        df = new float[collector.nodes.Count*2];
        int index = 0;
        foreach (GameObject node in collector.nodes)
        {
            df[index++] = node.GetComponent<NodeProperty>().dy;
            df[index++] = node.GetComponent<NodeProperty>().m;
        }
        string dfStr = "";
        foreach (float i in df) dfStr += (i + " ");
        Debug.Log("Degree of Freedom = "+dfStr);
    }

    void GenerateAllK()
    {
        k = new List<IndexMatrix>();
        foreach (GameObject member in collector.members)
        {
            MemberProperty property = member.GetComponent<MemberProperty>();

            List<int> index = new List<int>(){ property.node1.number * 2, property.node1.number * 2 + 1, property.node2.number * 2, property.node2.number * 2 + 1 };
            string indexStr = "";
            foreach (int i in index)
                indexStr += i + " ";
            Debug.Log("index = " + indexStr);

            float[,] kVal = GenerateK(member.GetComponent<MemberProperty>().number);
            
            k.Add(new IndexMatrix(index, kVal));
        }
    }

    float[,] GenerateK(int member)
    {
        float E = collector.members[member].GetComponent<MemberProperty>().GetE();
        float I = collector.members[member].GetComponent<MemberProperty>().GetI();
        float L = collector.members[member].GetComponent<MemberProperty>().length;

        float[,] k = new float[4, 4];
        float kMul = (E * I) / (L*L*L);
        k[0, 0] = 12;
        k[0, 1] = 6 * L;
        k[0, 2] = -12;
        k[0, 3] = 6 * L;
        k[1, 0] = 6 * L;
        k[1, 1] = 4 * L * L;
        k[1, 2] = -6 * L;
        k[1, 3] = 2 * L * L;
        k[2, 0] = -12;
        k[2, 1] = -6 * L;
        k[2, 2] = 12;
        k[2, 3] = -6 * L;
        k[3, 0] = 6 * L;
        k[3, 1] = 2 * L * L;
        k[3, 2] = -6 * L;
        k[3, 3] = 4 * L * L;
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                k[i, j] *= kMul;
        string kStr = "";
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                kStr += k[i, j] + " ";
            }
            kStr += "\n";
        }
        Debug.Log("k = \n"+kStr);
        return k;
    }

    void GenerateS()
    {
        List<int> availableIndex = FindAvailableDF();
        float[,] S = new float[availableIndex.Count, availableIndex.Count];
        for (int i = 0;i<availableIndex.Count;i++)
        {
            for (int j = 0;j<availableIndex.Count;j++)
            {
                S[i, j] = 0;
                foreach (IndexMatrix ki in k)
                {
                    S[i, j] += FindKValueByIndex(ki,availableIndex[i], availableIndex[j]);
                }
            }
        }

        s = new IndexMatrix(availableIndex, S);

        string sStr = "";
        for (int i = 0; i < availableIndex.Count; i++)
        {
            for (int j = 0; j < availableIndex.Count; j++)
                sStr += S[i, j] + " ";
            sStr += "\n";
        }

        Debug.Log("S = " + sStr);
    }

    float FindKValueByIndex(IndexMatrix kTarget,int x,int y)
    {
        int kIndexX = kTarget.index.IndexOf(x);
        int kIndexY = kTarget.index.IndexOf(y);
        if (kIndexX < 0 || kIndexY < 0) return 0;
        return kTarget.k_val[kIndexX, kIndexY];
    }

    List<int> FindAvailableDF()
    {
        List<int> availableIndex = new List<int>();
        for (int i = 0;i<df.Length;i++)
            if (df[i] == 1)
                availableIndex.Add(i);

        string indexStr = "";
        foreach (int i in availableIndex)
            indexStr += i + " ";
        Debug.Log("Available Index = " + indexStr);
        return availableIndex;
    }

    public void GeneratePi()
    {
        pi = new List<IndexArray>();
        foreach (GameObject member in collector.members)
        {
            MemberProperty property = member.GetComponent<MemberProperty>();
            List<int> index = new List<int>(){ property.node1.number * 2, property.node1.number * 2 + 1, property.node2.number * 2, property.node2.number * 2 + 1 };
            float[] piVal = new float[4];
            if (property.node1.support)
            {
                if (property.node1.pointLoad)
                    piVal[0] = -1 * property.node1.pointLoad.load;
                if (property.node1.momentum)
                    piVal[1] = property.node1.momentum.momentum;
            }
            if (property.node2.support)
            {
                if (property.node2.pointLoad)
                    piVal[2] = -1 * property.node2.pointLoad.load;
                if (property.node2.momentum)
                    piVal[3] = property.node2.momentum.momentum;
            }
            pi.Add(new IndexArray(index, piVal));
        }

        string piStr = "pi = \n";
        foreach (IndexArray piVal in pi)
        {
            foreach (float piValVal in piVal.val)
            {
                piStr += piValVal + " ";
            }
            piStr += "\n";
        }
        Debug.Log(piStr);
    }

    public void GenerateP()
    {
        List<int> availableIndex = FindAvailableDF();
        float[] pVal = new float[availableIndex.Count];
        for (int i = 0; i < availableIndex.Count; i++)
        {
            pVal[i] = 0;
            foreach (IndexArray piVal in pi)
            {
                int index = piVal.index.IndexOf(availableIndex[i]);
                if (index >= 0)
                    pVal[i] += piVal.val[index];
            }
        }
        p = new IndexArray(availableIndex, pVal);

        string pStr = "p = ";
        foreach (float i in p.val)
            pStr += i + " ";
        Debug.Log(pStr);
    }

    public void GenerateQF()
    {
        qf = new List<IndexArray>();
        
    }

    public void GeneratePF()
    {

    }

    public void ResetAnalyzer()
    {

    }
}
