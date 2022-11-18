using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator : RoomGenerator
{
    [SerializeField]
    protected DungeonParameter dungeonParameter;

    protected override void RunProceduralGeneration()
    {
        GenerateCorridors();
    }

    private void GenerateCorridors()
    {
        //Contains every kitchen tiles positions
        HashSet<Vector2Int> kitchenPositions = new HashSet<Vector2Int>();

        //Contains every floor tiles positions
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        //Contains every potentatial position for a room to be created
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        //Create the kitchen where the player will spawn
        CreateKitchen(kitchenPositions);

        //Create corridors from the kitchen
        CreateCorridors(floorPositions, potentialRoomPositions, kitchenPositions);

        //Create rooms from corridors extremities 
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        //Find where corridors lead nowhere
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        //Create rooms where corridors end
        CreateRoomsAtDeadEnds(deadEnds, roomPositions);

        //Update floor positions with room positions (and avoid doublons)
        floorPositions.UnionWith(roomPositions); 

        //Display dungeon floor
        tilemapVisualizer.PaintFloorTiles(floorPositions);

        //Display kitchen floor
        tilemapVisualizer.PaintKitchenFloorTiles(kitchenPositions);

        //Create walls
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);

    }

    private void CreateKitchen(HashSet<Vector2Int> kitchenPositions)
    {
        Debug.Log("Creating kitchen at " + startPosition.ToString());

        var currentPos = startPosition;
        kitchenPositions.Add(currentPos);

        int radius = dungeonParameter.kitchenSize / 2;

        for(int i = 0; i < radius; i++)
        {
            foreach (Vector2Int dir in Direction2D.eightDirections)
            {
                Vector2Int target = new Vector2Int(dir.x + (i * dir.x), dir.y + (i * dir.y));
                Debug.Log("Placing kitchen floor at " + (currentPos + target));
                kitchenPositions.Add(currentPos + target);
            }
        }
        

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

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions, HashSet<Vector2Int> kitchenPositions)
    {
        var currentPos = startPosition;

        if (kitchenPositions.Contains(currentPos))
        {
            potentialRoomPositions.Add(currentPos); //Adding actual position to potential room position
        }
        
        for(int i = 0; i < dungeonParameter.corridorCount; i++)
        {
            var corridor = RandomWalkAlgos.RandomWalkCorridor(currentPos, dungeonParameter.corridorLength);
            currentPos = corridor[corridor.Count - 1]; //Saving last position (at the end of the path)
            potentialRoomPositions.Add(currentPos); //The end of the corridor is a potential room
            floorPositions.UnionWith(corridor);
        }
    }
}
