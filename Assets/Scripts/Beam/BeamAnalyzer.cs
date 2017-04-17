using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAnalyzer : MonoBehaviour {

    BeamCollector collector;

    float[] df,pi;
    List<IndexMatrix> k;
    IndexMatrix s;
    List<IndexArray> qf;
    IndexArray pf,p,d;

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
        GenerateD();
        GenerateU();
        GenerateQ();
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

    void GeneratePi()
    {
        pi = new float[collector.nodes.Count*2];
        foreach (GameObject node in collector.nodes)
        {
            NodeProperty property = node.GetComponent<NodeProperty>();
            if (property.support)
            {
                if (property.pointLoad)
                    pi[property.number * 2] = property.pointLoad.load;
                if (property.momentum)
                    pi[property.number * 2 + 1] = property.momentum.momentum;
            }
        }

        string piStr = "pi = ";
        foreach (float piVal in pi)
        {
            piStr += piVal + " ";
        }
        Debug.Log(piStr);
    }

    void GenerateP()
    {
        List<int> availableIndex = FindAvailableDF();
        float[] pVal = new float[availableIndex.Count];
        for (int i = 0; i < availableIndex.Count; i++)
        {
            pVal[i] = pi[availableIndex[i]];
        }
        p = new IndexArray(availableIndex, pVal);

        string pStr = "p = ";
        foreach (float i in p.val)
            pStr += i + " ";
        Debug.Log(pStr);
    }

    void GenerateQF()
    {
        qf = new List<IndexArray>();
        //Point Load Case
        foreach (PointLoadProperty pointLoad in collector.pointLoads)
        {
            if (!collector.nodes[pointLoad.node].GetComponent<NodeProperty>().support)
            {
                float l1 = GetLengthOfLoad(pointLoad.node, false);
                float l2 = GetLengthOfLoad(pointLoad.node, true);
                int node1 = GetEndNodeIndex(pointLoad.node, false);
                int node2 = GetEndNodeIndex(pointLoad.node, true);
                List<int> index = new List<int>() { node1 * 2, node1 * 2 + 1, node2 * 2, node2 * 2 + 1 };
                float[] qfi = new float[4];
                qfi[0] += pointLoad.load * Mathf.Pow(l2, 2) * (3 * l1 + l2) / Mathf.Pow(l1 + l2, 3) * 1f;
                qfi[1] += pointLoad.load * l1 * Mathf.Pow(l2, 2) / Mathf.Pow(l1 + l2, 2) * 1f;
                qfi[2] += pointLoad.load * Mathf.Pow(l1, 2) * (l1 + 3 * l2) / Mathf.Pow(l1 + l2, 3) * 1f;
                qfi[3] += pointLoad.load * Mathf.Pow(l1, 2) * l2 / Mathf.Pow(l1 + l2, 2) * -1f;
                qf.Add(new IndexArray(index, qfi));
            }
        }
        
        //TODO Uniform Load
        foreach (UniformLoadProperty uniform in collector.uniformLoads)
        {
            MemberProperty member = collector.members[uniform.element].GetComponent<MemberProperty>();
            float l1 = GetLengthOfLoad(member.node2.number, false);
            float l2 = GetLengthOfLoad(member.node2.number, true);
            int node1 = GetEndNodeIndex(member.node2.number, false);
            int node2 = GetEndNodeIndex(member.node2.number, true);
            List<int> index = new List<int>() { node1 * 2, node1 * 2 + 1, node2 * 2, node2 * 2 + 1 };
            float[] qfi = new float[4];
            qfi[1] += uniform.load * Mathf.Pow(l2, 3) * (4f * (l1 + l2) - 3 * l2) / (12 * Mathf.Pow(l1 + l2,2));
            qfi[3] += uniform.load * Mathf.Pow(l2, 2) * (6 * Mathf.Pow(l1 + l2, 2) - 8 * (l1 + l2) * l2 + 3 * Mathf.Pow(l2, 2)) / (12 * (l1 + l2));

            qfi[0] += (qfi[1] - qfi[3] + uniform.load * Mathf.Pow(l2, 2) / 2) / (l1 + l2);
            qfi[2] += uniform.load * l2 - qfi[0];
            qf.Add(new IndexArray(index, qfi));
        }

        string qfStr = "qf = \n";
        foreach (IndexArray qfi in qf)
        {
            foreach (float qfiVal in qfi.val)
            {
                qfStr += qfiVal + " ";
            }
            qfStr += "\n";
        }
        Debug.Log(qfStr);
    }

    int GetEndNodeIndex(int node,bool isRight)
    {
        if (isRight)
        {
            if (node == collector.nodes.Count - 1) return node;
            if (collector.nodes[node].GetComponent<NodeProperty>().support) return node;
            return GetEndNodeIndex(node + 1, isRight);
        }else
        {
            if (node == 0) return node;
            if (collector.nodes[node].GetComponent<NodeProperty>().support) return node;
            return GetEndNodeIndex(node - 1, isRight);
        }
    }

    float GetLengthOfLoad(int node,bool isRight) 
    {
        if (isRight)
        {
            if (node == collector.nodes.Count) return 0;
            if (collector.nodes[node].GetComponent<NodeProperty>().support) return 0;
            return collector.nodes[node].GetComponent<NodeProperty>().rightMember.length + GetLengthOfLoad(node + 1, isRight);
        }else
        {
            if (node < 0) return 0;
            if (collector.nodes[node].GetComponent<NodeProperty>().support) return 0;
            return collector.nodes[node].GetComponent<NodeProperty>().leftMember.length + GetLengthOfLoad(node - 1, isRight);
        }
    }

    void GeneratePF()
    {
        List<int> availableIndex = FindAvailableDF();
        float[] pfVal = new float[availableIndex.Count];
        for (int i = 0; i < availableIndex.Count; i++)
        {
            foreach(IndexArray qfi in qf)
            {
                if (qfi.index.IndexOf(availableIndex[i]) >= 0)
                {
                    pfVal[i] += qfi.val[qfi.index.IndexOf(availableIndex[i])];
                }
            }
        }
        p = new IndexArray(availableIndex, pfVal);
        string pStr = "p = ";
        foreach (float pVal in p.val)
            pStr += pVal + " ";
        Debug.Log(pStr);
    }

    void GenerateD()
    {
        List<int> availableIndex = FindAvailableDF();
        float[] pdpf = new float[availableIndex.Count];
        for (int i = 0; i < availableIndex.Count; i++) {
            pdpf[i] = p.val[p.index.IndexOf(availableIndex[i])] - pf.val[pf.index.IndexOf(availableIndex[i])];
        }
        float[,] inverseS = new float[availableIndex.Count, availableIndex.Count];
        for (int i = 0;i< availableIndex.Count; i++)
        {
            for (int j = 0;j< availableIndex.Count; j++)
            {
                inverseS[i,j] = 1f / s.k_val[i,j];
            }
        }
        float[] dVal = new float[availableIndex.Count];
        for (int i = 0;i< availableIndex.Count; i++)
        {
            float sum = 0;
            for (int j = 0;j< availableIndex.Count; j++)
            {
                sum += inverseS[i, j] * pdpf[j];
            }
            dVal[i] = sum;
        }
        d = new IndexArray(availableIndex, dVal);

        string dStr = "d = ";
        foreach (float val in d.val)
        {
            dStr += val + " ";
        }
        Debug.Log(dStr);
    }
    
    void GenerateU()
    {

    }
    
    void GenerateQ()
    {

    }

    public void ResetAnalyzer()
    {

    }
}
