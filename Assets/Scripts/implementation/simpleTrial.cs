using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using UnityEngine.UI;

public class simpleTrial : MonoBehaviour
{
    public Text txt;

    private void Start()
    {
        int mRow = 5;
        int mCol = 6;
        int mElement = 3;
        var aM = Function.Matrix.Init(mRow, mCol);
        var bM = Function.Matrix.Init(mRow, mCol);
        var cM = Function.Matrix.Init(mRow, mCol);
        var container = Function.Matrix.Init(mRow * mCol, mElement);
        var y = Function.Matrix.Identity(3);

        #region A
        var v1 = aM.colVector[0];
        var v2 = aM.colVector[1];
        var v3 = aM.colVector[2];
        var v4 = aM.colVector[3];
        var v5 = aM.colVector[4];
        var v6 = aM.colVector[5];

        v1.vec[0] = 0;
        v1.vec[1] = 0;
        v1.vec[2] = 1;
        v1.vec[3] = 1;
        v1.vec[4] = 1;

        v2.vec[0] = 0;
        v2.vec[1] = 1;
        v2.vec[2] = 1;
        v2.vec[3] = 0;
        v2.vec[4] = 0;

        v3.vec[0] = 1;
        v3.vec[1] = 0;
        v3.vec[2] = 1;
        v3.vec[3] = 0;
        v3.vec[4] = 0;

        v4.vec[0] = 1;
        v4.vec[1] = 0;
        v4.vec[2] = 1;
        v4.vec[3] = 0;
        v4.vec[4] = 0;

        v5.vec[0] = 0;
        v5.vec[1] = 1;
        v5.vec[2] = 1;
        v5.vec[3] = 0;
        v5.vec[4] = 0;

        v6.vec[0] = 0;
        v6.vec[1] = 0;
        v6.vec[2] = 1;
        v6.vec[3] = 1;
        v6.vec[4] = 1;
        #endregion
        #region B
        v1 = bM.colVector[0];
        v2 = bM.colVector[1];
        v3 = bM.colVector[2];
        v4 = bM.colVector[3];
        v5 = bM.colVector[4];
        v6 = bM.colVector[5];

        v1.vec[0] = 0;
        v1.vec[1] = 0;
        v1.vec[2] = 0;
        v1.vec[3] = 0;
        v1.vec[4] = 0;

        v2.vec[0] = 1;
        v2.vec[1] = 1;
        v2.vec[2] = 1;
        v2.vec[3] = 1;
        v2.vec[4] = 1;

        v3.vec[0] = 1;
        v3.vec[1] = 0;
        v3.vec[2] = 1;
        v3.vec[3] = 0;
        v3.vec[4] = 1;

        v4.vec[0] = 1;
        v4.vec[1] = 0;
        v4.vec[2] = 1;
        v4.vec[3] = 0;
        v4.vec[4] = 1;

        v5.vec[0] = 1;
        v5.vec[1] = 1;
        v5.vec[2] = 1;
        v5.vec[3] = 1;
        v5.vec[4] = 1;

        v6.vec[0] = 0;
        v6.vec[1] = 0;
        v6.vec[2] = 0;
        v6.vec[3] = 0;
        v6.vec[4] = 0;
        #endregion
        #region C
        v1 = cM.colVector[0];
        v2 = cM.colVector[1];
        v3 = cM.colVector[2];
        v4 = cM.colVector[3];
        v5 = cM.colVector[4];
        v6 = cM.colVector[5];

        v1.vec[0] = 0;
        v1.vec[1] = 0;
        v1.vec[2] = 0;
        v1.vec[3] = 0;
        v1.vec[4] = 0;

        v2.vec[0] = 1;
        v2.vec[1] = 1;
        v2.vec[2] = 1;
        v2.vec[3] = 1;
        v2.vec[4] = 1;

        v3.vec[0] = 1;
        v3.vec[1] = 0;
        v3.vec[2] = 0;
        v3.vec[3] = 0;
        v3.vec[4] = 1;

        v4.vec[0] = 1;
        v4.vec[1] = 0;
        v4.vec[2] = 0;
        v4.vec[3] = 0;
        v4.vec[4] = 1;

        v5.vec[0] = 1;
        v5.vec[1] = 0;
        v5.vec[2] = 0;
        v5.vec[3] = 0;
        v5.vec[4] = 1;

        v6.vec[0] = 0;
        v6.vec[1] = 0;
        v6.vec[2] = 0;
        v6.vec[3] = 0;
        v6.vec[4] = 0;
        #endregion

        var aFlat = Function.Matrix.Flatten(aM);
        var bFlat = Function.Matrix.Flatten(bM);
        var cFlat = Function.Matrix.Flatten(cM);
        for (int i = 0; i < mRow * mCol; ++i)
        {
            container.colVector[0].vec[i] = aFlat.vec[i];
            container.colVector[1].vec[i] = bFlat.vec[i];
            container.colVector[2].vec[i] = cFlat.vec[i];
        }

        #region Input Display
        //DisplayMatrix(container);
        #endregion

        #region Output Display
        //DisplayMatrix(y);
        #endregion

        var weight1 = InitWeight(30, 5);
        var weight2 = InitWeight(5, 3);
        //DisplayMatrix(weight1);
        //DisplayMatrix(weight2);
        //Debug.Log(Function.Matrix.Shape(container)[0] + ", " + Function.Matrix.Shape(container)[1]);
        //DisplayMatrix(container);
        var forwardOutput = Forward(container, weight1, weight2);
        //DisplayMatrix(forwardOutput);

        float loss_i = loss(forwardOutput, y);
        //txt.text = loss_i.ToString();

        var backwardOutput = Backward(container, y, weight1, weight2, 0.01f);
        //DisplayMatrix(backwardOutput[0]);
    }
    internal void DisplayMatrix(Function.Matrix m)
    {
        StringBuilder note = new StringBuilder();
        note.Append("[");
        foreach (var i in m.colVector)
        {
            note.Append("[");
            foreach (var j in i.vec)
            {
                note.Append(j.ToString() + ", ");
            }
            note.Append("]");
        }
        note.Append("]");
        txt.text = note.ToString();
    }

    private Function.Matrix InitWeight(int inSize, int outSize)
    {
        var weightM = Function.Matrix.Init(inSize, outSize);
        for (int i = 0; i < outSize; ++i)
        {
            for (int j = 0; j < inSize; ++j)
            {
                weightM.colVector[i].vec[j] = UnityEngine.Random.Range(0f, 1f);
            }
        }
        return weightM;
    }

    private Function.Matrix Forward(Function.Matrix x, Function.Matrix w1, Function.Matrix w2)
    {
        //Debug.Log(Function.Matrix.Shape(x)[0] + ", " + Function.Matrix.Shape(x)[1]);
        var z1 = Function.Matrix.Multiply(Function.Matrix.Transpose(w1), x);
        //DisplayMatrix(z1);
        var a1 = Function.sigmoid(z1);

        var z2 = Function.Matrix.Multiply(Function.Matrix.Transpose(w2), a1);
        var a2 = Function.sigmoid(z2);
        return a2;
    }

    private float loss(Function.Matrix predict, Function.Matrix groundTruth)
    {
        var diff = Function.Matrix.Minus(predict, groundTruth);
        float result = 0f;
        foreach (var col in diff.colVector)
        {
            foreach (var element in col.vec)
            {
                result += element * element;
            }
        }
        var shape = Function.Matrix.Shape(diff);
        result /= shape[0] * shape[1];
        return result;
    }

    private Function.Matrix[] Backward(Function.Matrix x, Function.Matrix y, Function.Matrix w1, Function.Matrix w2, float alpha)
    {
        var z1 = Function.Matrix.Multiply(Function.Matrix.Transpose(w1), x);
        var a1 = Function.sigmoid(z1);

        var z2 = Function.Matrix.Multiply(Function.Matrix.Transpose(w2), a1);
        var a2 = Function.sigmoid(z2);

        var diff = Function.Matrix.Minus(a2, y);
        
        var w2Grad = Function.Matrix.Multiply(Function.Matrix.Multiply(a1, Function.Dsigmoid(z2)), diff);
        var w1Grad = Function.Matrix.Multiply(Function.Matrix.Transpose(w2), Function.Dsigmoid(z1));
        w1Grad = Function.Matrix.Multiply(x, Function.Matrix.Multiply(Function.Matrix.Multiply(w1Grad, Function.Dsigmoid(z2)), diff));
        //DisplayMatrix(Function.Matrix.Multiply(alpha, w1Grad));
        //Debug.Log(Function.Matrix.Dimension(w1));
        //Debug.Log(Function.Matrix.Shape(Function.Dsigmoid(w1Grad))[0] + ", " + Function.Matrix.Shape(Function.Dsigmoid(w1Grad))[1]);
        var w1Next = Function.Matrix.Minus(w1, Function.Matrix.Multiply(alpha, w1Grad));
        var w2Next = Function.Matrix.Minus(w2, Function.Matrix.Multiply(alpha, w2Grad));
        var result = new Function.Matrix[2];
        result[0] = w1Next;
        result[1] = w2Next;
        return result;
    }
}
