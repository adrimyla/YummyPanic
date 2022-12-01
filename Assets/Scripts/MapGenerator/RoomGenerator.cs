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

    protected override Dungeon RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(roomParam, startPosition);
        tilemapVisualizer.PaintFloorTiles(floorPositions); //Display floor
        WallGenerator.CreateWalls(floorPositions); //Display walls

        return new Dungeon();
    }

    protected HashSet<Vector2Int> RunRandomWalk(RoomParameter roomParam, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for(int i = 0; i < roomParam.iterations; i++)
        {
            var path = RandomWalkAlgos.SimpleRandomWalk(currentPosition, roomParam.walkLength);
            floorPositions.UnionWith(path); //Adding path to floorPositions and ignoring duplicates (Union)
            if (roomParam.startRandomly)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        return floorPositions;
    }
}
