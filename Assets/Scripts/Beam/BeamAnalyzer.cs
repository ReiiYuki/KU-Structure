using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAnalyzer : MonoBehaviour {

    BeamCollector collector;

    float[] df,pi,q;
    List<IndexMatrix> k;
    IndexMatrix s;
    List<IndexArray> qf,u,ku,qi;
    IndexArray pf,p,d,sfd,bmd;

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
        GenerateKU();
        GenerateQI();
        GenerateQ();
        GenerateSFDBMD();
    }
    #region DoF
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
    #endregion
    #region k
    void GenerateAllK()
    {
        k = new List<IndexMatrix>();
        List<int> index=new List<int>();
        float length = 0;
        int state = 0;
        foreach (GameObject node in collector.nodes)
        {
            NodeProperty property = node.GetComponent<NodeProperty>();
            if (state == 0)
            {
                index = new List<int>();
                index.Add(property.number * 2);
                index.Add(property.number * 2 + 1);
                length = property.rightMember.length;
                state = 1;
            }else if (state == 1)
            {
                if (property.support)
                {
                    index.Add(property.number * 2);
                    index.Add(property.number * 2 + 1);
                    float[,] val = GenerateK(property.leftMember, length);
                    k.Add(new IndexMatrix(index, val));

                    index = new List<int>();
                    index.Add(property.number * 2);
                    index.Add(property.number * 2 + 1);
                    if (property.rightMember)
                    {
                        length = property.rightMember.length;
                    }
                }
                else
                {
                    length += property.rightMember.length;
                }
            }
        }
    }

    float[,] GenerateK(MemberProperty member,float L)
    {
        Debug.Log("L = " + L);
        float E = member.GetE();
        float I = member.GetI();

        float[,] k = new float[4, 4];
        float kMul = (E * I) / (L*L*L);
        Debug.Log("E = " + E);
        Debug.Log("I = " + I);
        Debug.Log("EI/L^3 = " + kMul);

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
    #endregion
    #region S
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
    #endregion
    #region p
    void GeneratePi()
    {
        pi = new float[collector.nodes.Count*2];
        foreach (GameObject node in collector.nodes)
        {
            NodeProperty property = node.GetComponent<NodeProperty>();
            if (property.support)
            {
                if (property.pointLoad)
                    pi[property.number * 2] = -1*property.pointLoad.load;
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
    #endregion
    #region pf
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
            float L = l1 + l2;
            float w = uniform.load;
            int node1 = GetEndNodeIndex(member.node2.number, false);
            int node2 = GetEndNodeIndex(member.node2.number, true);
            Debug.Log("l1 = " + l1 + " l2 = " + l2 + " L = " + (l1 + l2) + " node1 = " + node1 + " Node2 = " + node2);
            List<int> index = new List<int>() { node1 * 2, node1 * 2 + 1, node2 * 2, node2 * 2 + 1 };
            float[] qfi = new float[4];

            qfi[1] = w * Mathf.Pow(l1, 2)*(6*Mathf.Pow(L,2)-8*L*l1+3*Mathf.Pow(l1,2))/(12*Mathf.Pow(L,2));
            qfi[3] = -1 * w * Mathf.Pow(l1, 3) * (4 * L - 3 * l1) / (12 * Mathf.Pow(L, 2));
            qfi[2] = (w * Mathf.Pow(l1, 2) / 2 - qfi[3] - qfi[1]) / L;
            qfi[0] = w * l1 - qfi[2];
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
        pf = new IndexArray(availableIndex, pfVal);
        string pfStr = "pf = ";
        foreach (float pfValue in pf.val)
            pfStr += pfValue + " ";
        Debug.Log(pfStr);
    }
    #endregion
    #region d
    void GenerateD()
    {
        List<int> availableIndex = FindAvailableDF();
        float[] pdpf = new float[availableIndex.Count];
        for (int i = 0; i < availableIndex.Count; i++) {
            pdpf[i] = p.val[p.index.IndexOf(availableIndex[i])] - pf.val[pf.index.IndexOf(availableIndex[i])];
        }
        string pdpfStr = "p-pf = ";
        foreach (float v in pdpf) pdpfStr += v + " ";
        Debug.Log(pdpfStr);
        float[,] inverseS = ConvertTo2D(InvertMatrix(ConvertTo2ArrD(s.k_val)));
        string sm1Str = "S^-1 = \n";
        for (int i = 0; i < availableIndex.Count; i++)
        {
            for (int j = 0; j < availableIndex.Count; j++)
            {
                sm1Str += inverseS[i, j] + " ";
            }
            sm1Str += "\n";
        }
        Debug.Log(sm1Str);
        float[] dVal = new float[availableIndex.Count];
        for (int i = 0;i< availableIndex.Count; i++)
        {
            float sum = 0;
            dVal[i] = 0;
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

    #region Convert Metrix
    float[][] ConvertTo2ArrD(float[,] arr2d)
    {
        float[][] newarr2d = new float[arr2d.GetLength(0)][];
        for (int i = 0;i< arr2d.GetLength(0); i++)
        {
            float[] arr = new float[arr2d.GetLength(0)];
            for (int j = 0;j< arr2d.GetLength(0); j++)
            {
                arr[j] = arr2d[i, j];
            }
            newarr2d[i] = arr;
        }
        return newarr2d;
    }

    float[,] ConvertTo2D(float[][] arr2d)
    {
        if (arr2d.Length == 0) return null;
        float[,] newarr2d = new float[arr2d.Length,arr2d[0].Length];
        for (int i = 0; i < arr2d.Length; i++)
        {
            for (int j = 0; j < arr2d[0].Length; j++)
            {
                newarr2d[i, j] = arr2d[i][j];
            }
        }
        return newarr2d;
    }
    #endregion
    #region inverse metrix
    float[][] InvertMatrix(float[][] A)
    {
        int n = A.Length;
        //e will represent each column in the identity matrix
        float[] e;
        //x will hold the inverse matrix to be returned
        float[][] x = new float[n][];
        for (int i = 0; i < n; i++)
        {
            x[i] = new float[A[i].Length];
        }
        /*
        * solve will contain the vector solution for the LUP decomposition as we solve
        * for each vector of x.  We will combine the solutions into the float[][] array x.
        * */
        float[] solve;

        //Get the LU matrix and P matrix (as an array)
        Tuple<float[][], int[]> results = LUPDecomposition(A);

        float[][] LU = results.First;
        int[] P = results.Second;

        /*
        * Solve AX = e for each column ei of the identity matrix using LUP decomposition
        * */
        for (int i = 0; i < n; i++)
        {
            e = new float[A[i].Length];
            e[i] = 1;
            solve = LUPSolve(LU, P, e);
            for (int j = 0; j < solve.Length; j++)
            {
                x[j][i] = solve[j];
            }
        }
        return x;
    }

    Tuple<float[][], int[]> LUPDecomposition(float[][] A)
    {
        int n = A.Length - 1;
        /*
        * pi represents the permutation matrix.  We implement it as an array
        * whose value indicates which column the 1 would appear.  We use it to avoid 
        * dividing by zero or small numbers.
        * */
        int[] pi = new int[n + 1];
        float p = 0;
        int kp = 0;
        int pik = 0;
        int pikp = 0;
        float aki = 0;
        float akpi = 0;

        //Initialize the permutation matrix, will be the identity matrix
        for (int j = 0; j <= n; j++)
        {
            pi[j] = j;
        }

        for (int k = 0; k <= n; k++)
        {
            /*
            * In finding the permutation matrix p that avoids dividing by zero
            * we take a slightly different approach.  For numerical stability
            * We find the element with the largest 
            * absolute value of those in the current first column (column k).  If all elements in
            * the current first column are zero then the matrix is singluar and throw an
            * error.
            * */
            p = 0;
            for (int i = k; i <= n; i++)
            {
                if (Mathf.Abs(A[i][k]) > p)
                {
                    p = Mathf.Abs(A[i][k]);
                    kp = i;
                }
            }
            if (p == 0)
            {
                throw new System.Exception("singular matrix");
            }
            /*
            * These lines update the pivot array (which represents the pivot matrix)
            * by exchanging pi[k] and pi[kp].
            * */
            pik = pi[k];
            pikp = pi[kp];
            pi[k] = pikp;
            pi[kp] = pik;

            /*
            * Exchange rows k and kpi as determined by the pivot
            * */
            for (int i = 0; i <= n; i++)
            {
                aki = A[k][i];
                akpi = A[kp][i];
                A[k][i] = akpi;
                A[kp][i] = aki;
            }

            /*
                * Compute the Schur complement
                * */
            for (int i = k + 1; i <= n; i++)
            {
                A[i][k] = A[i][k] / A[k][k];
                for (int j = k + 1; j <= n; j++)
                {
                    A[i][j] = A[i][j] - (A[i][k] * A[k][j]);
                }
            }
        }
        return Tuple.New(A, pi);
    }

    float[] LUPSolve(float[][] LU, int[] pi, float[] b)
    {
        int n = LU.Length - 1;
        float[] x = new float[n + 1];
        float[] y = new float[n + 1];
        float suml = 0;
        float sumu = 0;
        float lij = 0;

        /*
        * Solve for y using formward substitution
        * */
        for (int i = 0; i <= n; i++)
        {
            suml = 0;
            for (int j = 0; j <= i - 1; j++)
            {
                /*
                * Since we've taken L and U as a singular matrix as an input
                * the value for L at index i and j will be 1 when i equals j, not LU[i][j], since
                * the diagonal values are all 1 for L.
                * */
                if (i == j)
                {
                    lij = 1;
                }
                else
                {
                    lij = LU[i][j];
                }
                suml = suml + (lij * y[j]);
            }
            y[i] = b[pi[i]] - suml;
        }
        //Solve for x by using back substitution
        for (int i = n; i >= 0; i--)
        {
            sumu = 0;
            for (int j = i + 1; j <= n; j++)
            {
                sumu = sumu + (LU[i][j] * x[j]);
            }
            x[i] = (y[i] - sumu) / LU[i][i];
        }
        return x;
    }

    public class Tuple<T1, T2>
    {
        public T1 First { get; private set; }
        public T2 Second { get; private set; }
        internal Tuple(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }
    }

    public static class Tuple
    {
        public static Tuple<T1, T2> New<T1, T2>(T1 first, T2 second)
        {
            var tuple = new Tuple<T1, T2>(first, second);
            return tuple;
        }
    }
    #endregion
    void GenerateU()
    {
        u = new List<IndexArray>();
        foreach (IndexMatrix ki in k)
        {
            List<int> index = ki.index;
            float[] uVal = new float[4];
            for (int i = 0; i < 4; i++)
            {
                if (d.index.IndexOf(index[i]) >= 0)
                    uVal[i] = d.val[d.index.IndexOf(index[i])];
                else
                    uVal[i] = 0;
            }
            u.Add(new IndexArray(index, uVal));
        }

        string uStr = "u = \n";
        foreach (IndexArray ui in u)
        {
            foreach (float uij in ui.val)
                uStr += uij + " ";
            uStr += "\n";
        }
        Debug.Log(uStr);
    }
    #endregion
    #region ku
    void GenerateKU()
    {
        ku = new List<IndexArray>();
        for (int i = 0; i < k.Count; i++)
        {
            List<int> index = k[i].index;
            float[] val = new float[4];
            for (int j = 0; j < 4; j++)
            {
                for (int l = 0; l < 4; l++)
                {
                    val[j] += k[i].k_val[j, l] * u[i].val[l];
                }
            }
            ku.Add(new IndexArray(index, val));
        }

        string kuStr = "ku = \n";
        foreach (IndexArray kui in ku)
        {
            foreach (float val in kui.val)
            {
                kuStr += val + " ";
            }
            kuStr += "\n";
        }
        Debug.Log(kuStr);
    }
    #endregion
    #region q
    void GenerateQI()
    {
        qi = new List<IndexArray>();
        foreach (IndexArray kui in ku)
        {
            List<int> index = kui.index;
            float[] val = new float[4];
            if (qf.Count == 0)
            {
                val = kui.val;
            }
            else
            {
                foreach (IndexArray qfi in qf)
                {
                    if (ListEqual(index,qfi.index))
                    {
                        foreach (int qfiIndex in qfi.index)
                        {
                            if (index.IndexOf(qfiIndex) >= 0)
                            {
                                val[kui.index.IndexOf(qfiIndex)] = kui.val[index.IndexOf(qfiIndex)] + qfi.val[qfi.index.IndexOf(qfiIndex)];
                            }
                        }
                        break;
                    }else
                    {
                        val = kui.val;
                    }
                }
            }
            qi.Add(new IndexArray(index, val));
        }

        string qiStr = "qi = \n";
        foreach (IndexArray qii in qi)
        {
            foreach (float val in qii.val)
            {
                qiStr += val + " ";
            }
            qiStr += "\n";
        }
        Debug.Log(qiStr);
    }

    bool ListEqual(List<int> list1,List<int> list2)
    {
        if (list1.Count != list2.Count) return false;
        for (int i = 0; i < list1.Count; i++)
            if (list1[i] != list2[i])
                return false;
        return true;
    }

    void GenerateQ()
    {
        q = new float[collector.nodes.Count*2];
        foreach (GameObject node in collector.nodes)
        {
            NodeProperty property = node.GetComponent<NodeProperty>();
            q[property.number * 2] = 0;
            q[property.number * 2 + 1] = 0;
            foreach (IndexArray qii in qi)
            {
                if (qii.index.IndexOf(property.number * 2) >= 0)
                {
                    q[property.number * 2] += qii.val[qii.index.IndexOf(property.number * 2)];
                }
                if (qii.index.IndexOf(property.number * 2+1) >= 0)
                {
                    q[property.number * 2 + 1] += qii.val[qii.index.IndexOf(property.number * 2 + 1)];
                }
            }
        }

        string qStr = "q = ";
        foreach (float qi in q) qStr += qi + " ";
        Debug.Log(qStr);
    }

    #endregion

    #region sfd and bmd
    void GenerateSFDBMD()
    {
        float[] valSFD = new float[collector.nodes.Count];
        float[] valBMD = new float[collector.nodes.Count];
        List<int> indexSFD = new List<int>();
        List<int> indexBMD = new List<int>();
        foreach (GameObject node in collector.nodes)
        {
            NodeProperty property = node.GetComponent<NodeProperty>();
            indexSFD.Add(property.number * 2);
            indexBMD.Add(property.number * 2 + 1);
            valSFD[property.number] = q[property.number * 2];
            if (property.pointLoad&&property.support)
            {
                valSFD[property.number] += property.pointLoad.load;  
            }
            valSFD[property.number] = (float)System.Math.Round(valSFD[property.number],4);
            valBMD[property.number] = -1*(float)System.Math.Round(q[property.number * 2 + 1],4);
        }
        sfd = new IndexArray(indexSFD, valSFD);
        bmd = new IndexArray(indexBMD, valBMD);
        string sfdStr = "SFD = ";
        foreach (float sfdi in sfd.val) sfdStr += sfdi + " ";
        string bmdStr = "BMD = ";
        foreach (float bmdi in bmd.val) bmdStr += bmdi + " ";
        Debug.Log(sfdStr);
        Debug.Log(bmdStr);
    }
    #endregion
    public void ResetAnalyzer()
    {

    }
}
