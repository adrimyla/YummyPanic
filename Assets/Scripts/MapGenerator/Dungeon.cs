using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon
{
    public Kitchen kitchen;
    public HashSet<Vector2Int> floorPositions, wallPositions, potentialRoomPositions, roomPositions;
    public List<Vector2Int> deadEnds, freeFloorPositions;

    public Dungeon()
    {
        //Contains every floor tiles positions
        floorPositions = new HashSet<Vector2Int>();

        //Contains every wall tiles positions
        wallPositions = new HashSet<Vector2Int>();

        //Contains every potentatial position for a room to be created
        potentialRoomPositions = new HashSet<Vector2Int>();

        //Contains every room positions in dungeon
        roomPositions = new HashSet<Vector2Int>();

        //Contains every free tiles on map (no object or gluttons)
        freeFloorPositions = new List<Vector2Int>();

        //Contains every dead ends corridors
        deadEnds = new List<Vector2Int>();
      
    }

}
