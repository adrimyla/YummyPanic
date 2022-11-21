using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Kitchen
{
    int height, width;
    Vector2Int minPos, maxPos;
    Vector2 spawnPos;
    public HashSet<Vector2Int> floorPos; //Contains positions of every floor tile
    public HashSet<Vector2Int> wallPos; //Contains positions of every wall tiles

    public Kitchen(int kitchenHeight, int kitchenWidth, Vector2Int start)
    {
        height = kitchenHeight;
        width = kitchenWidth;
        floorPos = new HashSet<Vector2Int>();
        wallPos = new HashSet<Vector2Int>();    

        CreateFloor(start);
        CreateWalls();
    }

    private void CreateWalls()
    {
        Vector2Int initPos = new Vector2Int(minPos.x - 1, minPos.y - 1) ;
        HashSet<Vector2Int> walls = new HashSet<Vector2Int> ();
        walls.Add(initPos);

        //Top and Bottom
        for(int i = 0; i < maxPos.y + 2; i++)
        {
            walls.Add(initPos + new Vector2Int(i, minPos.x));
            walls.Add(initPos + new Vector2Int(i, maxPos.x + 1));
        }

        //Left and Right
        for (int i = 0; i < maxPos.y + 2; i++)
        {
            walls.Add(initPos + new Vector2Int(i, minPos.x));
            walls.Add(initPos + new Vector2Int(i, maxPos.x + 1));
        }

        wallPos.UnionWith(walls); //Add new walls to the final list
     
    }

    private void CreateFloor(Vector2Int startPos)
    {
        floorPos.Add(startPos); //Adding first tile as floor
        spawnPos = new Vector2Int(width / 2, height / 2); //Adding spawn position at center of the kitchen

        for (int i = 0; i < width; i++) //Width
        {
            for (int j = 0; j < height; j++) //Height
            {           
                var newPos = startPos + new Vector2Int(i, j); //Adding a floor tile
                floorPos.Add(newPos); //Adding tile to list with offset
            }
        }

        //Saving min and max pos
        minPos = startPos;
        maxPos = startPos + new Vector2Int(height, width);

    }

    public int GetHeight { get; }
    public int GetWidth { get; }
}
