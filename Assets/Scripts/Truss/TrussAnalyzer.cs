using UnityEngine;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class TrussAnalyzer : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

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
    }


    public void memberToArray(List<TrussMemberProperty> members,List<TrussNodeProperty> nodes)
    {
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
        float[,] sArray = new float[dnode.Count, dnode.Count];
        for (int i = 0; i < dnode.Count; i++)
            for (int j = 0; j < dnode.Count; j++)
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

        float[,] vArray = new float[members.Count,4];
        for (int i = 0; i < members.Count; i++)
            for (int j = 0; j < fIndex.Length; j++)
            {
                if (members[i].node1.number == fIndex[j])
                {
                    vArray[i, 0] = d.array[i * 2, 0];
                    vArray[i, 1] = d.array[i * 2+1, 0];
                }
                if (members[i].node2.number == fIndex[j])
                {
                    vArray[i, 2] = d.array[i * 2, 0];
                    vArray[i, 3] = d.array[i * 2+1, 0];
                }
            }

        Matrix2D[] U = new Matrix2D[members.Count];
        for (int i = 0; i < members.Count; i++)
        {
            U[i] = T[i] * new Matrix2D(vArray);
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

    }

    public int[] getForceIndex(List<TrussNodeProperty> nodes)
    {
        List<int> numbers = new List<int>();
        foreach (TrussNodeProperty node in nodes)
        {
            if (node.pointLoad.loadX != 0 || node.pointLoad.loadY != 0)
            {
                numbers.Add(node.number);
            }
        }
        return numbers.ToArray();
    }
    public Matrix2D getForce(List<TrussNodeProperty> nodes)
    {
        List<float> numbers = new List<float>();
        foreach (TrussNodeProperty node in nodes)
        {
            if (node.pointLoad.loadX != 0 || node.pointLoad.loadY != 0)
            {
                numbers.Add(node.pointLoad.loadX);
                numbers.Add(node.pointLoad.loadY);
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
