using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : RoomGenerator
{
    [SerializeField]
    protected DungeonParameter dungeonParameter;
    [SerializeField]
    private NavMeshBaking _navMeshBaker;

    protected override Dungeon RunProceduralGeneration()
    {       
        return CreateDunjeon();
    }

    private Kitchen GenerateKitchen(HashSet<Vector2Int> wallPositions)
    {
        //Find a place to create the kitchen (outside rooms)
        Vector2Int kitchenStartPos = FindAStartPositionForKitchen(startPosition, wallPositions);

        //Create the kitchen where the player will spawn
        Kitchen kitchen = new Kitchen(dungeonParameter.kitchenHeight, dungeonParameter.kitchenWidth, kitchenStartPos);
        
        kitchen.wallPos = WallGenerator.CreateWalls(kitchen.floorPos);

        return kitchen;

    } 

    private Vector2Int FindAStartPositionForKitchen(Vector2Int startPosition, HashSet<Vector2Int> wallPositions)
    {
        var currentPos = startPosition;

        //Find a wall going up
        while (!wallPositions.Contains(currentPos))
        {
            currentPos += Vector2Int.up; //If this is not a wall, we go up to find one
        }

        return currentPos;
    }

    private Dungeon CreateDunjeon()
    {
        Dungeon dg = new Dungeon();

        //Create corridors from the kitchen
        CreateCorridors(dg.floorPositions, dg.potentialRoomPositions);

        //Create rooms from corridors extremities 
        dg.roomPositions = CreateRooms(dg.potentialRoomPositions);

        //Find where corridors lead nowhere
        dg.deadEnds = FindAllDeadEnds(dg.floorPositions);

        //Create rooms where corridors end
        CreateRoomsAtDeadEnds(dg.deadEnds, dg.roomPositions);

        //Update floor positions with room positions (and avoid doublons)
        dg.floorPositions.UnionWith(dg.roomPositions);

        //Init free floor positions
        foreach (Vector2Int pos in dg.floorPositions)
        {
            dg.freeFloorPositions.Add(pos);
        }

        //Create dungeon walls
        dg.wallPositions = WallGenerator.CreateWalls(dg.floorPositions);

        //Creating kitchen
        dg.kitchen = GenerateKitchen(dg.wallPositions);

        //Removing invisible walls
        dg.wallPositions.ExceptWith(dg.kitchen.floorPos);
        dg.kitchen.wallPos.ExceptWith(dg.floorPositions);

        //Displaying dungeon tiles
        tilemapVisualizer.DisplayDungeon(dg.floorPositions, dg.wallPositions, dg.kitchen);

        // Setting up the NavMesh's surfave after all the tiles have been generated
        _navMeshBaker.ConfigureNavMesh();

        return dg;
    }

    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach(var pos in deadEnds)
        {
            if (!roomFloors.Contains(pos)) //If this dead end is not in a room
            {
                var room = RunRandomWalk(dungeonParameter.roomParam, pos); //Create a new room at this position
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
            foreach(var direction in Direction2D.fourDirections)
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
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * dungeonParameter.roomPercent); //Getting room count from percent parameter

        //Convert a HashSet to a Sorted List
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList(); //Create a new Global Unique Identifier (used to sort using this value)
   
        foreach(var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(dungeonParameter.roomParam, roomPosition);
            roomPositions.UnionWith(roomFloor); //Avoid room floor doublons
        }
        return roomPositions;
    
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPos = startPosition;

        for (int i = 0; i < dungeonParameter.corridorCount; i++)
        {
            var corridor = RandomWalkAlgos.RandomWalkCorridor(currentPos, dungeonParameter.corridorLength);
            currentPos = corridor[corridor.Count - 1]; //Saving last position (at the end of the path)
            potentialRoomPositions.Add(currentPos); //The end of the corridor is a potential room
            floorPositions.UnionWith(corridor);
        }
    }
}
