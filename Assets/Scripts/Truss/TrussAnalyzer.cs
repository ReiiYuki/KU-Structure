using UnityEngine;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class TrussAnalyzer : MonoBehaviour
{
    TRUSSCollector collector;
    public GameObject togglePanel;
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
        static float[,] MatrixProduct(float[,] matrixA, float[,] matrixB)
        {
            int aRows = matrixA.GetLength(0); int aCols = matrixA.GetLength(1);
            int bRows = matrixB.GetLength(0); int bCols = matrixB.GetLength(1);
            if (aCols != bRows)
                throw new Exception("Non-conformable matrices in MatrixProduct");

            float[,] result = new float[aRows,bCols];

            for (int i = 0; i <  aRows; ++i) // each row of A
        for (int j = 0; j < bCols; ++j) // each col of B
          for (int k = 0; k < aCols; ++k) // could use k less-than bRows
            result[i,j] += matrixA[i,k] * matrixB[k,j];

            //Parallel.For(0, aRows, i =greater-than
            //  {
            //    for (int j = 0; j less-than bCols; ++j) // each col of B
            //      for (int k = 0; k less-than aCols; ++k) // could use k less-than bRows
            //        result[i][j] += matrixA[i][k] * matrixB[k][j];
            //  }
            //);

            return result;
        }
        public static Matrix2D operator *(Matrix2D a, Matrix2D b)
        {
            return new Matrix2D(MatrixProduct(a.array,b.array));
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
        static T[,] To2D<T>(T[][] source)
        {
            try
            {
                int FirstDim = source.Length;
                int SecondDim = source.GroupBy(row => row.Length).Single().Key; // throws InvalidOperationException if source is not rectangular

                var result = new T[FirstDim, SecondDim];
                for (int i = 0; i < FirstDim; ++i)
                    for (int j = 0; j < SecondDim; ++j)
                        result[i, j] = source[i][j];

                return result;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("The given jagged array is not rectangular.");
            }
        }
        public static Matrix2D inverse(Matrix2D matrix)
        {
            
            return new Matrix2D(To2D(InvertMatrix(ToJaggedArray(matrix.array))));
        }


        public  Matrix2D trantranspose()
        {
            float[,] matric = new float[array.GetLength(1),array.GetLength(0)];
            Debug.Log(this);
            for (int i = 0; i < array.GetLength(1); i++)
                for (int j = 0; j < array.GetLength(0); j++)
                    matric[i, j] = array[j, i];
            Debug.Log(new Matrix2D(matric));
            return new Matrix2D(matric);
        }

        public override string ToString()
        {
            if (array == null) return null;
            string s = "";
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
        internal static T[][] ToJaggedArray<T>(T[,] twoDimensionalArray)
        {
            int rowsFirstIndex = twoDimensionalArray.GetLowerBound(0);
            int rowsLastIndex = twoDimensionalArray.GetUpperBound(0);
            int numberOfRows = rowsLastIndex + 1;

            int columnsFirstIndex = twoDimensionalArray.GetLowerBound(1);
            int columnsLastIndex = twoDimensionalArray.GetUpperBound(1);
            int numberOfColumns = columnsLastIndex + 1;

            T[][] jaggedArray = new T[numberOfRows][];
            for (int i = rowsFirstIndex; i <= rowsLastIndex; i++)
            {
                jaggedArray[i] = new T[numberOfColumns];

                for (int j = columnsFirstIndex; j <= columnsLastIndex; j++)
                {
                    jaggedArray[i][j] = twoDimensionalArray[i, j];
                }
            }
            return jaggedArray;
        }
        public static float[][] InvertMatrix(float[][] A)
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

        public static Tuple<float[][], int[]> LUPDecomposition(float[][] A)
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

        public static float[] LUPSolve(float[][] LU, int[] pi, float[] b)
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
    }


    public void analyze()
    {
        togglePanel.SetActive(false);
        // init members
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
            Debug.Log("sin: "+sin);
            Debug.Log("cos: "+cos);
            Debug.Log("cos2: "+cos2);
            Debug.Log("sin2: "+sin2);
            Debug.Log("cossin: "+cos);
            Matrix2D matrix = new Matrix2D(new float[,] {
                    {cos2,cossin,-cos2,-cossin},
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

            // float EAL = members[i].GetE() * members[i].GetI() / members[i].lenghtIn(); // first case
            float EAL = members[i].GetE() * members[i].GetI() / members[i].lenght();
            Debug.Log("EAL: " + EAL);
            Matrix2D matrixK = new Matrix2D(new float[,] {
                    {EAL,0,-EAL,0},
                    {0,0,0,0},
                    {-EAL,0,EAL,0},
                    {0,0,0,0}
                });
            // Debug.Log(members[i].GetE()+" "+ members[i].GetI()+" "+ members[i].lenght());
            // Debug.Log(EAL);
            Debug.Log(matrix);
            matrix = matrix * EAL;
            matrixs[i] = matrix;
            T[i] = matrixT;
            K[i] = matrixK;
        }
        foreach (Matrix2D m in matrixs)
            Debug.Log(m);
        List<TrussNodeProperty> dnode = getPoint(nodes);
        Debug.Log(dnode.Count);
        //float[,] sArray = new float[dnode.Count*2, dnode.Count*2];
        //for (int i = 0; i < dnode.Count; i++)
        //    for (int j = 0; j < dnode.Count; j++)
        //        for (int k = 0; k < members.Count; k++)
        //        {
        //            Debug.Log(members[k].node1.Equals(dnode[i])+ " " + members[k].node1.Equals(dnode[j])+" "+ members[k].node2.Equals(dnode[i])+" "+ members[k].node2.Equals(dnode[j]));
        //            if ((members[k].node1.Equals(dnode[i])&& members[k].node1.Equals(dnode[j]))||
        //                (members[k].node2.Equals(dnode[i]) && members[k].node2.Equals(dnode[j])))
        //            {
        //                sArray[i * 2, j * 2] += matrixs[k].array[0, 0];
        //                sArray[i * 2+1, j * 2] += matrixs[k].array[1, 0];
        //                sArray[i * 2, j * 2+1] += matrixs[k].array[0, 1];
        //                sArray[i * 2+1, j * 2+1] += matrixs[k].array[1, 1];
        //                Debug.Log("X");
        //                Debug.Log(new Matrix2D(sArray));
        //            }
        //            if ((members[k].node2.Equals(dnode[i])&& members[k].node1.Equals(dnode[j]))||
        //                (members[k].node1.Equals(dnode[i]) && members[k].node2.Equals(dnode[j])))
        //            {
        //                sArray[i * 2, j * 2] += matrixs[k].array[2, 0];
        //                sArray[i * 2 + 1, j * 2] += matrixs[k].array[3, 0];
        //                sArray[i * 2, j * 2 + 1] += matrixs[k].array[2, 3];
        //                sArray[i * 2 + 1, j * 2 + 1] += matrixs[k].array[3, 3];
        //                Debug.Log("Y");
        //                Debug.Log(new Matrix2D(sArray));
        //            }
        //        }
        //for (int l = 0; l < dfi.Length; l++)
        //    for (int m = 0; m < members.Count; m++)
        int[] dfi = getIndex(nodes);
        float[,] sArray = new float[dfi.Length, dfi.Length];
        for (int i = 0; i < dfi.Length; i++)
            for (int j = 0; j < dfi.Length; j++)
                for (int k = 0; k < members.Count; k++)

                {
                    Debug.Log(dfi[i] + " " + dfi[j] + " " + members[k].node1.number + " " + members[k].node2.number);
                    Debug.Log(matrixs[k]);
                    if (members[k].node1.number * 2 == dfi[i])
                    {
                        if (members[k].node1.number * 2 == dfi[j])
                            sArray[i, j] += matrixs[k].array[0, 0];
                        else if (members[k].node1.number * 2 + 1 == dfi[j])
                            sArray[i, j] += matrixs[k].array[0, 1];
                        else if (members[k].node2.number * 2 == dfi[j])
                            sArray[i, j] += matrixs[k].array[0, 2];
                        else if (members[k].node2.number * 2 + 1 == dfi[j])
                            sArray[i, j] += matrixs[k].array[0, 3];
                    }
                    else if (members[k].node1.number * 2 + 1 == dfi[i])
                    {
                        if (members[k].node1.number * 2 == dfi[j])
                            sArray[i, j] += matrixs[k].array[1, 0];
                        else if (members[k].node1.number * 2 + 1 == dfi[j])
                            sArray[i, j] += matrixs[k].array[1, 1];
                        else if (members[k].node2.number * 2 == dfi[j])
                            sArray[i, j] += matrixs[k].array[1, 2];
                        else if (members[k].node2.number * 2 + 1 == dfi[j])
                            sArray[i, j] += matrixs[k].array[1, 3];
                    }
                    else if (members[k].node2.number * 2 == dfi[i])
                    {
                        if (members[k].node1.number * 2 == dfi[j])
                            sArray[i, j] += matrixs[k].array[2, 0];
                        else if (members[k].node1.number * 2 + 1 == dfi[j])
                            sArray[i, j] += matrixs[k].array[2, 1];
                        else if (members[k].node2.number * 2 == dfi[j])
                            sArray[i, j] += matrixs[k].array[2, 2];
                        else if (members[k].node2.number * 2 + 1 == dfi[j])
                            sArray[i, j] += matrixs[k].array[2, 3];
                    }
                    else if (members[k].node2.number * 2 + 1 == dfi[i])
                    {
                        if (members[k].node1.number * 2 == dfi[j])
                            sArray[i, j] += matrixs[k].array[3, 0];
                        else if (members[k].node1.number * 2 + 1 == dfi[j])
                            sArray[i, j] += matrixs[k].array[3, 1];
                        else if (members[k].node2.number * 2 == dfi[j])
                            sArray[i, j] += matrixs[k].array[3, 2];
                        else if (members[k].node2.number * 2 + 1 == dfi[j])
                            sArray[i, j] += matrixs[k].array[3, 3];
                    }
                }
        Debug.Log("5555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555");
        Debug.Log(sArray.GetLength(0));
        Debug.Log(sArray.GetLength(1));
        Debug.Log(dfi.Length);
        Debug.Log(new Matrix2D(sArray));
        Matrix2D sArrayI = Matrix2D.inverse(new Matrix2D(sArray));
        Matrix2D fnodes = getForce(nodes);
        Debug.Log(sArrayI);
        Debug.Log(fnodes);
        Matrix2D d = sArrayI* fnodes;
        int[] fIndex = getForceIndex(nodes);
        Debug.Log(d);
        Debug.Log(d.array.GetLength(0));
        Debug.Log(d.array.GetLength(1));
        Debug.Log(fIndex.Length);
        Matrix2D[] V = new Matrix2D[members.Count];
        for (int i = 0; i < members.Count; i++)
        {
            for (int j = 0; j < dfi.Length; j++)
            {
                float[,] vArray = new float[4, 1];
                Debug.Log("I: " + i + " J: " + j);
                Debug.Log(d);
                if (dfi.Contains<int>(members[i].node1.number * 2 ))
                    vArray[0, 0] = d.array[Array.IndexOf(dfi, members[i].node1.number * 2), 0];
                if (dfi.Contains<int>(members[i].node1.number * 2+1 ))
                    vArray[1, 0] = d.array[Array.IndexOf(dfi, members[i].node1.number * 2+1), 0];
                if (dfi.Contains<int>(members[i].node2.number * 2))
                    vArray[2, 0] = d.array[Array.IndexOf(dfi, members[i].node2.number * 2), 0];
                if (dfi.Contains<int>(members[i].node2.number * 2+1))
                    vArray[3, 0] = d.array[Array.IndexOf(dfi, members[i].node2.number * 2+1), 0];
                V[i] = new Matrix2D(vArray);
                Debug.Log(V[i]);
                Debug.Log("aaaaaaaaaaaaaaa");
            }
        }
        foreach (Matrix2D m in V)
            Debug.Log(m);
        Matrix2D[] U = new Matrix2D[members.Count];
        for (int i = 0; i < members.Count; i++)
        {
            Debug.Log("T: \n"+T[i]);
            Debug.Log("V: \n"+V[i]);
            U[i] = T[i] * V[i];
        }

        Matrix2D[] Q = new Matrix2D[members.Count];
        for (int i = 0; i < members.Count; i++)
        {
            Debug.Log("K: \n" + K[i]);
            Debug.Log("U: \n" + U[i]);
            Q[i] = K[i] * U[i];
        }
        Matrix2D[] TT = new Matrix2D[members.Count];
        for (int i = 0; i < members.Count; i++)
        {
            TT[i] = T[i].trantranspose();
        }

        Matrix2D[] F = new Matrix2D[members.Count];
        for (int i = 0; i < members.Count; i++)
        {
            Debug.Log(Q[i]);
            Debug.Log(TT[i]);
            F[i] = TT[i]*Q[i];
        }
        Debug.Log("###################");
        foreach (Matrix2D f in F)
            Debug.Log(f);
        Debug.Log("###################");

        for(int i =0;i<members.Count;i++)
        {
            if(Q[i].array[0, 0]< 0)
            {
                collector.AddQ(members[i], members[i].node1.number, Q[i].array[0, 0],true);
                //collector.AddQ(members[i], members[i].node2.number, Q[i].array[2, 0],true);
                if(members[i].CanDesignCheck())
                    collector.streeRatio(members[i], tension(Q[i].array[0, 0], members[i].GetI(), members[i].GetFy()),true);
            }
            else
            {
                collector.AddQ(members[i], members[i].node1.number, Q[i].array[0, 0],false);
                //collector.AddQ(members[i], members[i].node2.number, Q[i].array[2, 0],false);
                if(members[i].CanDesignCheck())
                    collector.streeRatio(members[i],compression(members[i].lenght(), members[i].GetE(), members[i].GetFy(), members[i].GetR(), members[i].GetI(), Q[i].array[0, 0]),true);
            }
            
            collector.AddForce(members[i].node1.number, F[i].array[0, 0],F[i].array[1,0]);
            collector.AddForce(members[i].node2.number, F[i].array[2, 0], F[i].array[3, 0]);
        }
        togglePanel.SetActive(true);
    }

    public int[] getForceIndex(List<TrussNodeProperty> nodes)
    {
        List<int> numbers = new List<int>();
        foreach (TrussNodeProperty node in nodes)
        {
            //if (node.pointLoadX != null || node.pointLoadY != null)
            //{
            //    numbers.Add(node.number);
            //}
            if (node.support == null)
                numbers.Add(node.number);
        }

        Debug.Log(numbers.Count);
        return numbers.ToArray();
    }
    public Matrix2D getForce(List<TrussNodeProperty> nodes)
    {
        List<float> numbers = new List<float>();
        foreach (TrussNodeProperty node in nodes)
        {
            if (node.dx == 1)
                if (node.pointLoadX != null)
                    numbers.Add(node.pointLoadX.load);
                else
                    numbers.Add(0);

            if (node.dy == 1)
                if (node.pointLoadY != null)
                    numbers.Add(node.pointLoadY.load);
                else
                    numbers.Add(0);
            
        }
        Debug.Log(numbers.Count);
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
            Debug.Log("dx: " + node.dx + " dy: " + node.dy+" node_index: "+node.number);
            if (node.dx == 1 || node.dy == 1)
                newList.Add(node);
        }
        return newList;
    }
    public int[] getIndex(List<TrussNodeProperty> nodes)
    {
        List<int> il = new List<int>();
        foreach(TrussNodeProperty n in nodes)
        {
            if (n.dx == 1)
                il.Add(n.number*2);
            if (n.dy == 1)
                il.Add(n.number * 2 + 1);
            Debug.Log(n.dx+" "+n.dy);
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        return il.ToArray();
    }
    private float calAngle(float x1,float x2,float lenght)
    {
        Debug.Log(x2 + "-" + x1 + "/" + lenght);
        return (x2 - x1) /lenght;
    }

    private char tension(float T,float A,float fy)
    {
        float stressRatio = (T / A) / (fy * 0.6f);
        if (stressRatio >= 0 && stressRatio < 0.5)
            return 'l';
        if (stressRatio >= 0.5 && stressRatio <= 1)
            return 'm';
        if (stressRatio < 0)
            return 'n';
        return 'h';
    }
    private char compression(float length,float E,float Fy,float r,float A,float C)
    {
        float cc = (float)Math.Sqrt((2 * Math.PI * Math.PI * E) / Fy);
        float KLr = length / r;
        float Fa = (float)(12f * Math.PI * Math.PI * E) / (23f * KLr * KLr);
        if (cc>KLr)
            Fa = (float)(1f- (1f/2f)* (KLr* KLr)/cc) / ((5f/3f)+(3f/8f)*(KLr/cc)-(1f/8f)*(KLr/cc)* (KLr / cc)* (KLr / cc));
        float stressRatio = C / A / Fa;
        if (stressRatio >= 0 && stressRatio < 0.5)
            return 'l';
        if (stressRatio >= 0.5 && stressRatio <= 1)
            return 'm';
        if (stressRatio < 0)
            return 'n';
        return 'h';
    }
}
