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

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnds(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions); //Avoid room doublons

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);

    }

    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach(var pos in deadEnds)
        {
            if (!roomFloors.Contains(pos)) //If this dead end is not in a room
            {
                var room = RunRandomWalk(randomWalkParam, pos); //Create a new room at this position
                roomFloors.UnionWith(room); //Add the room to roomFloors list
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach(var pos in floorPositions)
        {
            int neighboursCount = 0;
            foreach(var direction in Direction2D.cardinalDirectionList)
            {
                if (floorPositions.Contains(pos + direction)) //If there is floor next to this floor
                    neighboursCount++;                
            }

            if (neighboursCount==1) //If there is only one direction
                deadEnds.Add(pos);
        }

        return deadEnds;
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
