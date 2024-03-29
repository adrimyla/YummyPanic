using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomWalkAlgos
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var previousPosition = startPosition;

        for(int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for(int i = 0; i < corridorLength; i++) //Create a corridor with corridorLength
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }
}

public static class Direction2D
{
    //Contains different direction on a 2D map
    public static List<Vector2Int> fourDirections = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(0,-1), //DOWN
        new Vector2Int(-1,0), //LEFT
    };

    //Contains different direction on a 2D map
    public static List<Vector2Int> eightDirections = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,1), //UP RIGHT
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(1,-1), //DOWN RIGHT
        new Vector2Int(0,-1), //DOWN
        new Vector2Int(-1,-1), //DOWN LEFT
        new Vector2Int(-1,0), //LEFT
        new Vector2Int(-1,1), //UP LEFT

    };

    //Return a random direction
    public static Vector2Int GetRandomCardinalDirection()
    {
        return fourDirections[Random.Range(0, fourDirections.Count)];
    }
}