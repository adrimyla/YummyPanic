using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonParam_", menuName = "DungeonGenerator/DungeonParameter")]
public class DungeonParameter : ScriptableObject
{
    [Header("Kitchen")]
    public int kitchenHeight = 5;
    public int kitchenWidth = 5;

    [Header("Corridors")]
    public int corridorLength = 14;
    public int corridorCount = 5;

    [Header("Rooms")]
    public RoomParameter roomParam;
    [Range(0.1f, 1)]
    public float roomPercent = 0.8f;

}
