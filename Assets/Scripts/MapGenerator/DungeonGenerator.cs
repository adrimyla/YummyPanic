using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    private DungeonData data;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        tilemapVisualizer.Clear(); //Clear display
        tilemapVisualizer.PaintFloorTiles(floorPositions); //Display floor
        WallGenerator.CreateWalls(floorPositions,tilemapVisualizer); //Display walls
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for(int i = 0; i < data.iterations; i++)
        {
            var path = ProcGenAlgos.SimpleRandomWalk(currentPosition, data.walkLength);
            floorPositions.UnionWith(path); //Adding path to floorPositions and ignoring duplicates (Union)
            if (data.startRandomly)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        return floorPositions;
    }
}
