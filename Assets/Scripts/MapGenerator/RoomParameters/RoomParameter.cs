using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomParam_", menuName = "DungeonGenerator/RoomParameter")]
public class RoomParameter : ScriptableObject
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomly = true;
}
