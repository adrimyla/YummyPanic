using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    private RoomParameter roomParam;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(roomParam, startPosition);
        tilemapVisualizer.PaintFloorTiles(floorPositions); //Display floor
        WallGenerator.CreateWalls(floorPositions,tilemapVisualizer); //Display walls
    }

    protected HashSet<Vector2Int> RunRandomWalk(RoomParameter parameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for(int i = 0; i < parameters.iterations; i++)
        {
            var path = RandomWalkAlgos.SimpleRandomWalk(currentPosition, parameters.walkLength);
            floorPositions.UnionWith(path); //Adding path to floorPositions and ignoring duplicates (Union)
            if (parameters.startRandomly)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        return floorPositions;
    }
}
