using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class test : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        var m = Function.Matrix.RandomMatrix(2, 3);
        DisplayMatrix(m);
        //Debug.Log(Function.Matrix.Dimension(m));
    }

    internal void DisplayMatrix(Function.Matrix m)
    {
        int row = Function.Matrix.Shape(m)[0];
        int col = Function.Matrix.Shape(m)[1];
        Debug.Log(row + ", " + col);
        StringBuilder note = new StringBuilder();
        note.Append("[");
        for (int i = 0; i < row; ++i)
        {
            note.Append("[");
            for (int j = 0; j < col; ++j)
            {
                note.Append(m.colVector[j].vec[i].ToString() + ", ");
            }
            note.Append("]");
        }
        note.Append("]");
        text.text = note.ToString();
    }
}
