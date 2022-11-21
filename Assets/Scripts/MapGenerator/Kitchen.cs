using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen
{
    int height, width;
    public HashSet<Vector2Int> floorPos;
    public HashSet<Vector2Int> wallPos;

    public Kitchen(int kitchenHeight, int kitchenWidth, Vector2Int start)
    {
        height = kitchenHeight;
        width = kitchenWidth;
        floorPos = new HashSet<Vector2Int>();
        wallPos = new HashSet<Vector2Int>();    

        CreateFloor(start);
        //TODO : CreateWalls();
    }

    private void CreateFloor(Vector2Int startPos)
    {
        var newPos = startPos;
        Vector2Int offset = new Vector2Int(width / 2, height / 2);

        floorPos.Add(newPos);

        for (int i = 0; i < width; i++) //Width
        {
            for (int j = 0; j < height; j++) //Height
            {
                newPos = startPos + new Vector2Int(i, j);
                floorPos.Add(newPos - offset);
            }
        }
    }

    public int GetHeight { get; }
    public int GetWidth { get; }
}
