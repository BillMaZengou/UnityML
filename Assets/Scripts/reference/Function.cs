using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Function
{
    public static float sigmoid(float input)
    {
        float expResult = Mathf.Exp(input);
        float output = expResult / (expResult + 1f);
        return output;
    }
    public static Vector sigmoid(Vector input)
    {
        var output = Vector.Init(Vector.Len(input));
        for (int i = 0; i < Vector.Len(input); ++i)
        {
            output.vec[i] = sigmoid(input.vec[i]);
        }
        return output;
    }
    public static Matrix sigmoid(Matrix input)
    {
        var output = Matrix.Init(Matrix.Shape(input)[0], Matrix.Shape(input)[1]);
        for (int i = 0; i < Matrix.Shape(input)[1]; ++i)
        {
            output.colVector[i] = sigmoid(input.colVector[i]);
        }
        return output;
    }
    public static float Dsigmoid(float input)
    {
        var sig = sigmoid(input);
        return sig * (1 - sig);
    }
    public static Vector Dsigmoid(Vector input)
    {
        var sig = sigmoid(input);
        var diff = Vector.Minus(Vector.Ones(Vector.Len(sig)), sig);
        var output = Vector.Init(Vector.Len(input));
        for (int i = 0; i < Vector.Len(input); ++i)
        {
            output.vec[i] = sig.vec[i] * diff.vec[i];
        }
        return output;
    }
    public static Matrix Dsigmoid(Matrix input)
    {
        var sig = sigmoid(input);
        var diff = Matrix.Minus(Matrix.Ones(Matrix.Shape(sig)[0], Matrix.Shape(sig)[1]), sig);
        var output = Matrix.Init(Matrix.Shape(input)[0], Matrix.Shape(input)[1]);
        for (int i = 0; i < Matrix.Shape(input)[1]; ++i)
        {
            output.colVector[i] = Dsigmoid(input.colVector[i]);
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
        public static Vector Init(int size)
        {
            var m = new Vector
            {
                vec = new float[size]
            };
            return m;
        }
        public static Vector Zeros(int size)
        {
            var result = Init(size);
            for (int i = 0; i < size; ++i)
            {
                result.vec[i] = 0f;
            }
            return result;
        }
        public static Vector Ones(int size)
        {
            var result = Init(size);
            for (int i = 0; i < size; ++i)
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
                Vector w = Init(Len(u));
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
        public static Matrix Init(int row, int col)
        {
            var m = new Matrix
            {
                colVector = new Vector[col]
            };
            for (int i = 0; i < col; ++i)
            {
                m.colVector[i] = Vector.Init(row);
            }
            return m;
        }
        public static Matrix Zeros(int row, int col)
        {
            var result = Init(row, col);
            for (int i = 0; i < col; ++i)
            {
                result.colVector[i] = Vector.Zeros(row);
            }
            return result;
        }
        public static Matrix Ones(int row, int col)
        {
            var result = Init(row, col);
            for (int i = 0; i < col; ++i)
            {
                result.colVector[i] = Vector.Ones(row);
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
        public static int Dimension(Matrix m)
        {
            var size = Shape(m);
            return size[0] * size[1];
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
            if (Shape(m)[0] != Shape(n)[0] && Shape(m)[1] != Shape(n)[1])
            {
                Debug.LogError("Two matrices have different shape.");
                return null;
            }
            else
            {
                var colNumber = Shape(m)[1];
                Matrix w = Init(Shape(m)[0], colNumber);
                for (int i = 0; i < colNumber; ++i)
                {
                    w.colVector[i] = Vector.Add(m.colVector[i], n.colVector[i]);
                }
                return w;
            }
        }
        public static Matrix Minus(Matrix m, Matrix n)
        {
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
                Debug.Log(mColNumber.ToString() + " x " + nRowNumber.ToString());
                Debug.LogError("Two matrices cannot multiply.");
                return null;
            }
            else
            {
                var mul = Init(Shape(m)[0], Shape(n)[1]);
                for (int i = 0; i < Shape(n)[1]; ++i)
                {
                    var tempVec = Multiply(m, n.colVector[i]);
                    mul.colVector[i] = tempVec;
                }
                return mul;
            }
        }
        public static Vector Flatten(Matrix m)
        {
            var shape = Shape(m);
            int size = shape[0] * shape[1];
            //Debug.Log(size);
            var flattenVec = Vector.Init(size);
            for (int i = 0; i < size; ++i)
            {
                int row = 0;
                int col;
                if (i < shape[1])
                {
                    col = i;
                }
                else
                {
                    row = Mathf.FloorToInt(i / shape[1]);
                    col = i - row * shape[1];
                }
                //Debug.Log(i);
                Debug.Log(row);
                Debug.Log(col);
                flattenVec.vec[i] = m.colVector[col].vec[row];
            }
            return flattenVec;
        }
        public static Matrix Transpose(Matrix m)
        {
            var mShape = Shape(m);
            var trans = Init(mShape[1], mShape[0]);
            for (int i = 0; i < mShape[1]; ++i)
            {
                for (int j = 0; j < mShape[0]; ++j)
                {
                    trans.colVector[j].vec[i] = m.colVector[i].vec[j];
                }
            }
            return trans;
        }
        public static Matrix RandomMatrix(int row, int col)
        {
            var result = Init(row, col);
            for (int j = 0; j < col; ++j)
            {
                for (int i = 0; i < row; ++i)
                {
                    result.colVector[j].vec[i] = UnityEngine.Random.Range(0f, 1f);
                }
            }
            return result;
        }
    }
}
