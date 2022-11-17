using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGen : RandomWalkGen
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    public float roomPercent = 0.8f;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        floorPositions.UnionWith(roomPositions); //Avoid room doublons

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);

    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent); //Getting room count from percent parameter

        //Convert a HashSet to a Sorted List
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList(); //Create a new Global Unique Identifier (used to sort using this value)
   
        foreach(var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParam, roomPosition);
            roomPositions.UnionWith(roomFloor); //Avoid room floor doublons
        }
        return roomPositions;
    
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPos = startPosition;
        potentialRoomPositions.Add(currentPos); //Adding actual position to potential room position

        for(int i = 0; i < corridorCount; i++)
        {
            var corridor = ProcGenAlgos.RandomWalkCorridor(currentPos, corridorLength);
            currentPos = corridor[corridor.Count - 1]; //Saving last position (at the end of the path)
            potentialRoomPositions.Add(currentPos); //The end of the corridor is a potential room
            floorPositions.UnionWith(corridor);
        }
    }
}
