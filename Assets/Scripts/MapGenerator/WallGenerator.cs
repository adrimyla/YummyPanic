using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static HashSet<Vector2Int> CreateWalls(HashSet<Vector2Int> floorPositions)
    {
        var basicWallsPositions = FindWallsInDirections(floorPositions, Direction2D.fourDirections);

        return basicWallsPositions;
    }

    //Find out if a tile is a wall
    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach(var position in floorPositions)
        {
            foreach(var direction in directionsList)
            {
                var neighbourPosition = position + direction; //Exemple : (1,1) + UP = (2,1)
                if (!floorPositions.Contains(neighbourPosition)) //If there is no floor at (2,1)
                    wallPositions.Add(neighbourPosition); //So the tile at (2,1) is a wall 
            }
        }

        return wallPositions;
    }
}
