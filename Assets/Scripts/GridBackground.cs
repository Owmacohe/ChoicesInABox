using System;
using UnityEngine;

public class GridBackground : MonoBehaviour
{
    [SerializeField] Vector2 gridSize = new Vector2(100, 100);
    [SerializeField] GameObject gridLine;

    void Start()
    {
        Vector2 half = gridSize / 2f;

        for (int i = -(int) half.x; i < half.x; i++)
        {
            Instantiate(gridLine, new Vector2(i, 0), Quaternion.Euler(0, 0, 0), transform);
            
            for (int j = -(int) half.y; j < half.y; j++)
            {
                Instantiate(gridLine, new Vector2(0, j), Quaternion.Euler(0, 0, 90), transform);
            }
        }
    }
}