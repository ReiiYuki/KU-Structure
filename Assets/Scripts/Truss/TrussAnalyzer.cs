using UnityEngine;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class TrussAnalyzer : MonoBehaviour
{
    TRUSSCollector collector;
    // Use this for initialization
    void Start()
    {
        collector = GetComponent<TRUSSCollector>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public struct Matrix2D
    {
        public float[,] array;

        public Matrix2D(float[,] array)
        {
            this.array = array;
        }
        public static Matrix2D operator +(Matrix2D a, Matrix2D b) {
            float[,] newArray = new float[a.array.GetLength(0), b.array.GetLength(1)];
            for (int i = 0; i < a.array.GetLength(0); i++)
                for (int j = 0; j < b.array.GetLength(1); j++)
                    newArray[i, j] = a.array[i, j] + b.array[i, j];

            return new Matrix2D(newArray);
        }
        public static Matrix2D operator -(Matrix2D a)
        {
            float[,] newArray = new float[a.array.GetLength(0), a.array.GetLength(1)];
            for (int i = 0; i < a.array.GetLength(0); i++)
                for (int j = 0; j < a.array.GetLength(1); j++)
                    newArray[i, j] = -a.array[i, j];

            return new Matrix2D(newArray);
        }
        public static Matrix2D operator -(Matrix2D a, Matrix2D b)
        {
            float[,] newArray = new float[a.array.GetLength(0), b.array.GetLength(1)];
            for (int i = 0; i < a.array.GetLength(0); i++)
                for (int j = 0; j < b.array.GetLength(1); j++)
                    newArray[i, j] = a.array[i, j] - b.array[i, j];

            return new Matrix2D(newArray);
        }
        public static Matrix2D operator *(Matrix2D a, Matrix2D b)
        {
            float[,] newArray = new float[a.array.GetLength(0), b.array.GetLength(1)];
            for (int i = 0; i < a.array.GetLength(0); i++)
                for (int j = 0; j < b.array.GetLength(1); j++)
                    newArray[i, j] = a.array[i, j] * b.array[j, i];

            return new Matrix2D(newArray);
        }
        public static Matrix2D operator *(Matrix2D a, float d)
        {

            float[,] newArray = new float[a.array.GetLength(0), a.array.GetLength(1)];
            for (int i = 0; i < a.array.GetLength(0); i++)
                for (int j = 0; j < a.array.GetLength(1); j++)
                    newArray[i, j] = a.array[i, j] * d;

            return new Matrix2D(newArray);
        }
        public static Matrix2D operator *(float d, Matrix2D a)
        {
            return a * d;
        }
        public static Matrix2D operator /(Matrix2D a, float d)
        {
            float[,] newArray = new float[a.array.GetLength(0), a.array.GetLength(1)];
            for (int i = 0; i < a.array.GetLength(0); i++)
                for (int j = 0; j < a.array.GetLength(1); j++)
                    newArray[i, j] = a.array[i, j] / d;

            return new Matrix2D(newArray);
        }
        public static bool operator ==(Matrix2D a, Matrix2D b)
        {
            if (a.array.GetLongLength(0) != b.array.GetLongLength(0))
                return false;
            if (a.array.GetLongLength(1) != b.array.GetLongLength(1))
                return false;
            for (int i = 0; i < a.array.GetLength(0); i++)
                for (int j = 0; j < a.array.GetLength(1); j++)
                    if (a.array[i, j] != b.array[i, j])
                        return false;

            return true;
        }
        public static bool operator !=(Matrix2D lhs, Matrix2D rhs)
        {
            return !(lhs == rhs);
        }
        public static Matrix2D operator ^(Matrix2D a, Matrix2D b)
        {
            float[,] newArray = new float[a.array.GetLength(0), b.array.GetLength(1)];
            for (int i = 0; i < a.array.GetLength(0); i++)
                for (int j = 0; j < b.array.GetLength(0); j++)
                    for(int k = 0;k <b.array.GetLength(1); k++)
                        newArray[i, j] = a.array[i, k]*b.array[k,j];

            return new Matrix2D(newArray);
        }

        public static Matrix2D inverse(Matrix2D matrix)
        {
            float[,] array = new float[matrix.array.GetLength(0), matrix.array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    array[i, j] = -matrix.array[i, j];

            return new Matrix2D(array);
        }
        public  Matrix2D trantranspose()
        {
            float[,] matric = new float[array.GetLength(1),array.GetLength(0)];
            for (int i = 0; i < array.GetLength(1); i++)
                for (int j = 0; j < array.GetLength(0); j++)
                    matric[i, j] = matric[j, i]; ;
            return new Matrix2D(matric);
        }

        public override string ToString()
        {
            string s = "";
            Debug.Log(array);
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    s += (array[i, j] + ",");
                }
                s += "\n";
            }
            return s;
        }
    }


    public void analyze()
    {

        List<TrussMemberProperty> members = new List<TrussMemberProperty>();
        foreach (GameObject g in collector.members)
            members.Add(g.GetComponent<TrussMemberProperty>());


        List<TrussNodeProperty> nodes = new List<TrussNodeProperty>();
        foreach (GameObject g in collector.nodes)
            nodes.Add(g.GetComponent<TrussNodeProperty>());
        float[][] array = new float[members.Count][];
        Matrix2D[] matrixs = new Matrix2D[members.Count];
        Matrix2D[] T = new Matrix2D[members.Count];
        Matrix2D[] K = new Matrix2D[members.Count];
        for (int i = 0;i < members.Count;i++)
        {

            TrussNodeProperty node1 = members[i].node1;
            TrussNodeProperty node2 = members[i].node2;

            float cos = calAngle(node1.x, node2.x, members[i].lenght());
            float sin = calAngle(node1.y, node2.y, members[i].lenght());
            float cos2 = Mathf.Pow(cos, 2);
            float sin2 = Mathf.Pow(sin, 2);
            float cossin = cos * sin;
            Matrix2D matrix = new Matrix2D(new float[,] {
                    {cos2,cossin,cos2,-cossin},
                    {cossin,sin2,-cossin,-sin2},
                    {-cos2,-cossin,cos2,cossin},
                    {-cossin,-sin2,cossin,sin2}
                });
            Matrix2D matrixT = new Matrix2D(new float[,] {
                    {cos,sin,0,0},
                    {-sin,cos,0,0},
                    {0,0,cos,sin},
                    {0,0,-sin,cos}
                });

            float EAL = members[i].GetE() * members[i].GetI() / members[i].lenght();

            Matrix2D matrixK = new Matrix2D(new float[,] {
                    {EAL,0,-EAL,0},
                    {0,0,0,0},
                    {-EAL,0,EAL,0},
                    {0,0,0,0}
                });
            matrix = matrix * EAL;
            matrixs[i] = matrix;
            T[i] = matrixT;
            K[i] = matrixK;
        }
        List<TrussNodeProperty> dnode = getPoint(nodes);
        float[,] sArray = new float[dnode.Count*2, dnode.Count*2];
        for (int i = 0; i < dnode.Count-1; i++)
            for (int j = 0; j < dnode.Count-1; j++)
                for (int k = 0; k < members.Count; k++)
                {
                    if ((members[k].node1.Equals(dnode[i])&& members[k].node1.Equals(dnode[j]))||
                        (members[k].node2.Equals(dnode[i]) && members[k].node2.Equals(dnode[j])))
                    {
                        sArray[i * 2, j * 2] += matrixs[k].array[0, 0];
                        sArray[i * 2+1, j * 2] += matrixs[k].array[1, 0];
                        sArray[i * 2, j * 2+1] += matrixs[k].array[0, 1];
                        sArray[i * 2+1, j * 2+1] += matrixs[k].array[1, 1];
                    }
                    if ((members[k].node2.Equals(dnode[i])&& members[k].node1.Equals(dnode[j]))||
                        (members[k].node1.Equals(dnode[i]) && members[k].node2.Equals(dnode[j])))
                    {
                        sArray[i * 2, j * 2] += matrixs[k].array[2, 0];
                        sArray[i * 2 + 1, j * 2] += matrixs[k].array[3, 0];
                        sArray[i * 2, j * 2 + 1] += matrixs[k].array[2, 3];
                        sArray[i * 2 + 1, j * 2 + 1] += matrixs[k].array[3, 3];
                    }


                }

        Matrix2D sArrayI = Matrix2D.inverse(new Matrix2D(sArray));
        Matrix2D fnodes = getForce(nodes);
        Matrix2D d = fnodes*sArrayI;
        int[] fIndex = getForceIndex(nodes);
        Debug.Log(fIndex[0]);
        Matrix2D[] V = new Matrix2D[members.Count];
        for (int i = 0; i < members.Count; i++)
            for (int j = 0; j < fIndex.Length; j++)
            {
                float[,] vArray = new float[4, 1];
                if (members[i].node1.number == fIndex[j])
                {
                    vArray[0, 0] = d.array[i * 2, 0];
                    vArray[1, 0] = d.array[i * 2+1, 0];
                }
                if (members[i].node2.number == fIndex[j])
                {
                    vArray[2, 0] = d.array[i * 2, 0];
                    vArray[3, 0] = d.array[i * 2+1, 0];
                }
                V[i] = new Matrix2D(vArray);
                Debug.Log(V[i]);
                Debug.Log("aaaaaaaaaaaaaaa");
                break;
            }

        Matrix2D[] U = new Matrix2D[members.Count];
        for (int i = 0; i < members.Count; i++)
        {
            Debug.Log(T[i].array.GetLength(0));
            Debug.Log(T[i].array.GetLength(1));
            Debug.Log(T[i]);
            Debug.Log(V[i] == null);
            U[i] = T[i] * V[i];
        }

        Matrix2D[] Q = new Matrix2D[members.Count];
        for (int i = 0; i < members.Count; i++)
        {
            U[i] = K[i] * U[i];
        }
        Matrix2D[] TT = new Matrix2D[members.Count];
        for (int i = 0; i < members.Count; i++)
        {
            TT[i] = T[i];
        }

        Matrix2D[] F = new Matrix2D[members.Count];
        for (int i = 0; i < members.Count; i++)
        {
            F[i] = TT[i]*Q[i];
        }
        foreach (Matrix2D f in F)
            Debug.Log(f.array);
        Debug.Log(F.Length);
    }

    public int[] getForceIndex(List<TrussNodeProperty> nodes)
    {
        Debug.Log(nodes.Count);
        for (int i = 0; i < nodes.Count; i++)
            Debug.Log(nodes[i].pointLoadX);
        List<int> numbers = new List<int>();
        foreach (TrussNodeProperty node in nodes)
        {
            Debug.Log(node.pointLoadX.load);
            Debug.Log(node.pointLoadY.load);
            Debug.Log("aaaaaaaaaaaaaa");
            if (node.pointLoadX.load != 0 || node.pointLoadY.load != 0)
            {
                numbers.Add(node.number);
            }
        }
        Debug.Log(numbers.Count);
        return numbers.ToArray();
    }
    public Matrix2D getForce(List<TrussNodeProperty> nodes)
    {
        List<float> numbers = new List<float>();
        foreach (TrussNodeProperty node in nodes)
        {
            if (node.pointLoadX.load != 0 || node.pointLoadY.load != 0)
            {
                numbers.Add(node.pointLoadX.load);
                numbers.Add(node.pointLoadY.load);
            }
        }
        float[,] array = new float[numbers.Count,1];
        for(int i=0;i<numbers.Count;i++)
        {
            array[i, 0] = numbers[i];
        }
        return new Matrix2D(array);
            
    }
    public List<TrussNodeProperty> getPoint(List<TrussNodeProperty> nodes)
    {
        List<TrussNodeProperty> newList = new List<TrussNodeProperty>();
        foreach(TrussNodeProperty node in nodes)
        {
            if (node.dx == 1)
                newList.Add(node);
        }
        return newList;
    }

    private float calAngle(float x1,float x2,float lenght)
    {
        return (x2 - x1) /lenght;
    }

}
