using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonParam_", menuName = "DungeonGenerator/DungeonParameter")]
public class DungeonParameter : ScriptableObject
{
    public RoomParameter roomParam;
    public int corridorLength = 14, corridorCount = 5;
    [Range(0.1f, 1)]
    public float roomPercent = 0.8f;
}
