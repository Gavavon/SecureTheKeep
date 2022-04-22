using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnSetUp : MonoBehaviour
{

    public Transform[] columns;
    public int numColumns;

    // Start is called before the first frame update
    void Start()
    {
        numColumns = columns.Length;
        for (int i = 1; i < columns.Length; i++)
        {
            columns[i].position = columns[i - 1].position + new Vector3(170, 0, 0);
        }
    }
}
