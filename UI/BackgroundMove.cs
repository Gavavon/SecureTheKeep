using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public Transform page;
    public int pageColumnsCount;
    public float pageSpeed;
    public GameObject parent;
    private Transform[,] background = new Transform[5, 5];

    void Start()
    {
        for (int j = 0; j < background.GetLength(0); j++) 
        {
            for (int i = 0; i < background.GetLength(1); i++)
            {
                Transform clone;
                clone = Instantiate(page, page.transform.position + new Vector3(i * (170 * pageColumnsCount) + (j * 180), j * 780, 0), transform.rotation);
                clone.SetParent(parent.transform);
                background[j, i] = clone;
            }
        }
        parent.transform.position = new Vector3(0,0,0);
        page.GetComponent<CanvasGroup>().alpha = 0;
    }

    void Update()
    {
        for (int j = 0; j < background.GetLength(0); j++)
        {
            for (int i = 0; i < background.GetLength(1); i++)
            {
                background[j, i].position += new Vector3(-1, -2, 0) * Time.deltaTime * pageSpeed;
            }
        }
    }

    private void FixedUpdate()
    {
        for (int j = 0; j < background.GetLength(0); j++)
        {
            for (int i = 0; i < background.GetLength(1); i++)
            {
                if (background[j, i].position.y < -1500)
                {
                    resetRow(j);
                    break;
                }
                if (background[j, i].position.x < -1250)
                {
                    resetColumn(i);
                    break;
                }
            }
        }
    }

    public void resetRow(int row)
    {
        int temp = row;
        if (temp == 0)
        {
            temp = background.GetLength(0) - 1;
        }
        else 
        {
            temp = row - 1;
        }
        for (int i = 0; i < background.GetLength(0); i++)
        {
            background[row, i].position = background[temp, i].position + new Vector3(180, 780, 0);
        }
    }

    public void resetColumn(int column) 
    {
        int temp = column;
        if (temp == 0)
        {
            temp = background.GetLength(1) - 1;
        }
        else
        {
            temp = column - 1;
        }
        for (int i = 0; i < background.GetLength(1); i++)
        {
            background[i, column].position = background[i, temp].position + new Vector3((170 * pageColumnsCount), 0, 0);
        }
    }
    
}
