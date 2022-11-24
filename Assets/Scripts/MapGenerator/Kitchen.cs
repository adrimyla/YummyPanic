using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Kitchen
{
    int height, width;
    int outsideCorridorLength = 10;
    Vector2Int minPos, maxPos;
    public HashSet<Vector2Int> floorPos; //Contains positions of every floor tile
    public HashSet<Vector2Int> wallPos; //Contains positions of every wall tiles
    public HashSet<Vector2Int> kitchenPos; //Contains positions of kitchen area

    public Kitchen(int kitchenHeight, int kitchenWidth, Vector2Int start)
    {
        //Initializing
        height = kitchenHeight;
        width = kitchenWidth;
        floorPos = new HashSet<Vector2Int>();
        wallPos = new HashSet<Vector2Int>();
        kitchenPos = new HashSet<Vector2Int>();

        //Calculating kitchen area
        minPos = start + new Vector2Int(-(width / 2), 0);
        maxPos = start + new Vector2Int(width / 2, height + outsideCorridorLength);

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

        //Creating outside corridor
        for(int i = 0; i < outsideCorridorLength; i++)
        {
            var newPos = startPos + new Vector2Int(0, i); //Adding a floor tile
            floorPos.Add(newPos);
        }

        Vector2Int offset = new Vector2Int(-(width / 2), outsideCorridorLength); //Placing kitchen room at the end of entry corridor
        for (int i = 0; i < width; i++) //Width
        {
            for (int j = 0; j < height; j++) //Height
            {           
                var newPos = startPos + new Vector2Int(i, j); //Adding a floor tile
                floorPos.Add(newPos + offset); //Adding tile to list with offset
            }
        }

        //Adding floor positions to kitchen area
        kitchenPos.AddRange(floorPos);

    }

    public int GetHeight { get; }
    public int GetWidth { get; }
}
