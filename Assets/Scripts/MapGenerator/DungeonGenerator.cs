using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    [SerializeField]
    private GeneratorData randomWalkParameters;
    
    [SerializeField]
    private TilemapVisualizer tilemapVisualizer;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for(int i = 0; i < iterations; i++)
        {
            var path = ProcGenAlgos.SimpleRandomWalk(currentPosition, walkLength);
            floorPositions.UnionWith(path); //Adding path to floorPositions and ignoring duplicates (Union)
            if (startRandomly)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        return floorPositions;
    }
}
