using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAnalyzer : MonoBehaviour {

    BeamCollector collector;

    float[] df;
    List<k> kList;
    struct k
    {
        public int[] index;
        public float[,] k_val;
        public k(int[] index,float[,] k_val)
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
        string dfStr = "";
        foreach (float i in df) dfStr += (i + " ");
        Debug.Log("Degree of Freedom = "+dfStr);
    }

    public void GenerateAllK()
    {
        kList = new List<k>();
        foreach (GameObject member in collector.members)
        {
            MemberProperty property = member.GetComponent<MemberProperty>();
            float[,] kVal = GenerateK(member.GetComponent<MemberProperty>().number);
            int[] index = { property.node1.number * 2, property.node1.number * 2 + 1, property.node2.number * 2, property.node2.number * 2 + 1 };
            kList.Add(new k(index, kVal));
        }
    }

    public float[,] GenerateK(int member)
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

    public void ResetAnalyzer()
    {

    }
}
