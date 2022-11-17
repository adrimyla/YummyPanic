using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorFirstDungeonGen : RandomWalkGen
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    public float roomPercent = 0.8f;
    [SerializeField]
    public DungeonData roomGenParam;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstDungeonGeneration();
    }

    private void CorridorFirstDungeonGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions);

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);

    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions)
    {
        var currentPos = startPosition;

        for(int i = 0; i < corridorCount; i++)
        {
            var corridor = ProcGenAlgos.RandomWalkCorridor(currentPos, corridorLength);
            currentPos = corridor[corridor.Count - 1]; //Saving last position (at the end of the path)
            floorPositions.UnionWith(corridor);
        }
    }
}
