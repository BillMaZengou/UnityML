using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Function
{
    public static float sigmoid(float input)
    {
        float expResult = Mathf.Exp(input);
        float output = expResult / (expResult + 1f);
        return output;
    }
    public static Vector sigmoid(Vector input)
    {
        Vector output = new Vector();
        for (int i = 0; i < Vector.Len(input); ++i)
        {
            output.vec[i] = sigmoid(input.vec[i]);
        }
        return output;
    }
    public static Matrix sigmoid(Matrix input)
    {
        Matrix output = new Matrix();
        for (int i = 0; i < Matrix.Shape(input)[1]; ++i)
        {
            output.colVector[i] = sigmoid(input.colVector[i]);
        }
        return output;
    }
    public static float lineFitting(float x, float m, float c)
    {
        float y = m * x + c;
        return y;
    }
    public static Vector lineFitting(Vector x, Matrix m, Vector c)
    {
        Vector y = Vector.Add(Matrix.Multiply(m, x), c);
        return y;
    }

    public class Vector
    {
        public float[] vec;
        public static Vector Zeros(int size)
        {
            var result = new Vector
            {
                vec = new float[size],
            };
            Array.Clear(result.vec, 0, Len(result));
            return result;
        }
        public static Vector Ones(int size)
        {
            var result = new Vector
            {
                vec = new float[size],
            };
            for (int i = 0; i < Len(result); ++i)
            {
                result.vec[i] = 1f;
            }
            return result;
        }
        public static int Len(Vector u)
        {
            return u.vec.Length;
        }
        public static float Distance(Vector v)
        {
            float result = 0f;
            foreach (float i in v.vec)
            {
                result += i * i;
            }
            return result;
        }
        public static Vector Add(Vector u, Vector v)
        {
            if (Len(v) != Len(u))
            {
                Debug.LogError(v.ToString() + " and " + u.ToString() + " have different size.");
                return null;
            }
            else
            {
                Vector w = new Vector();
                for (int i = 0; i < Len(u); ++i)
                {
                    w.vec[i] += u.vec[i] + v.vec[i];
                }
                return w;
            }
        }
        public static Vector Minus(Vector u, Vector v)
        {
            if (Len(v) != Len(u))
            {
                Debug.LogError(v.ToString() + " and " + u.ToString() + " have different size.");
                return null;
            }
            var vMinus = v;
            for (int i = 0; i < Len(v); ++i)
            {
                vMinus.vec[i] = -v.vec[i];
            }
            return Vector.Add(u, vMinus);
        }
        public static Vector Multiply(float a, Vector v)
        {
            var vMul = v;
            for (int i = 0; i < Len(v); ++i)
            {
                vMul.vec[i] = a*v.vec[i];
            }
            return vMul;
        }
        public static Vector Multiply(Vector v, float a)
        {
            return Multiply(a, v);
        }
        public static Vector Devide(Vector v, float a)
        {
            var vDev = v;
            for (int i = 0; i < Len(v); ++i)
            {
                vDev.vec[i] = v.vec[i]/a;
            }
            return vDev;
        }
        public static float Dot(Vector u, Vector v)
        {
            float result = 0f;
            if (Len(v) != Len(u))
            {
                Debug.LogError(v.ToString() + " and " + u.ToString() + " have different size.");
                return Mathf.Infinity;
            }
            else
            {
                for (int i = 0; i < Len(u); ++i)
                {
                    result += u.vec[i] * v.vec[i];
                }
                return result;
            }
        }
    }

    public class Matrix
    {
        public Vector[] colVector;
        public static Matrix Zeros(int row, int col)
        {
            var tempVec = Vector.Zeros(row);
            var result = new Matrix
            {
                colVector = new Vector[col],
            };
            for (int i = 0; i < col; ++i)
            {
                result.colVector[i] = tempVec;
            }
            return result;
        }
        public static Matrix Ones(int row, int col)
        {
            var tempVec = Vector.Ones(row);
            var result = new Matrix
            {
                colVector = new Vector[col],
            };
            for (int i = 0; i < col; ++i)
            {
                result.colVector[i] = tempVec;
            }
            return result;
        }
        public static Matrix Identity(int dimension)
        {
            var result = Zeros(dimension, dimension);
            for (int i = 0; i < dimension; ++i)
            {
                result.colVector[i].vec[i] = 1f;
            }
            return result;
        }
        public static int[] Shape(Matrix m)
        {
            int col = m.colVector.Length;
            int row = m.colVector[0].vec.Length;
            int[] size = new int[] { row, col };
            return size;
        }
        public static float Element(Matrix m, int rowIdx, int colIdx)
        {
            return m.colVector[colIdx].vec[rowIdx];
        }
        public static Vector Colume(Matrix m, int colIdx)
        {
            return m.colVector[colIdx];
        }
        public static Vector Row(Matrix m, int rowIdx)
        {
            var rowVector = new Vector();
            var colNumber = Shape(m)[1];
            for (int i = 0; i < colNumber; ++i)
            {
                rowVector.vec[i] = m.colVector[i].vec[rowIdx];
            }
            return rowVector;
        }
        public static Matrix Add(Matrix m, Matrix n)
        {
            if (Shape(m) != Shape(n))
            {
                Debug.LogError("Two matrices have different shape.");
                return null;
            }
            else
            {
                var colNumber = Shape(m)[1];
                Matrix w = new Matrix();
                for (int i = 0; i < colNumber; ++i)
                {
                    w.colVector[i] = Vector.Add(m.colVector[i], n.colVector[i]);
                }
                return w;
            }
        }
        public static Matrix Minus(Matrix m, Matrix n)
        {
            if (Shape(m) != Shape(n))
            {
                Debug.LogError("Two matrices have different shape.");
                return null;
            }
            var nMinus = n;
            var colNumber = Shape(n)[1];
            for (int i = 0; i < colNumber; ++i)
            {
                nMinus.colVector[i] = Vector.Multiply(-1, n.colVector[i]);
            }
            return Add(m, nMinus);
        }
        public static Matrix Multiply(float a, Matrix n)
        {
            var nMul = n;
            var colNumber = Shape(n)[1];
            for (int i = 0; i < colNumber; ++i)
            {
                nMul.colVector[i] = Vector.Multiply(a, n.colVector[i]);
            }
            return nMul;
        }
        public static Matrix Multiply(Matrix n, float a)
        {
            return Multiply(a, n);
        }
        public static Matrix Devide(Matrix n, float a)
        {
            var nDev = n;
            var colNumber = Shape(n)[1];
            for (int i = 0; i < colNumber; ++i)
            {
                nDev.colVector[i] = Vector.Devide(n.colVector[i], a);
            }
            return nDev;
        }
        public static Vector Multiply(Matrix m, Vector x)
        {
            var colNumber = Shape(m)[1];
            if (colNumber != Vector.Len(x))
            {
                Debug.LogError("Matrix and vector have different dimension.");
                return null;
            }
            else
            {
                var mMul = Vector.Multiply(x.vec[0], m.colVector[0]);
                for (int i = 1; i < colNumber; ++i)
                {
                    var tempVec = Vector.Multiply(x.vec[i], m.colVector[i]);
                    mMul = Vector.Add(mMul, tempVec);
                }
                return mMul;
            }
        }
        public static Matrix Multiply(Matrix m, Matrix n)
        {
            var mColNumber = Shape(m)[1];
            var nRowNumber = Shape(n)[0];
            if (mColNumber != nRowNumber)
            {
                Debug.LogError("Two matrices cannot multiply.");
                return null;
            }
            else
            {
                var mul = new Matrix();
                for (int i = 0; i < Shape(n)[1]; ++i)
                {
                    var tempVec = Multiply(m, n.colVector[i]);
                    mul.colVector[i] = tempVec;
                }
                return mul;
            }
        }
    }
}
