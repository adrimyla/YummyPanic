using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonData_", menuName = "DungeonGenerator/DungeonData")]
public class DungeonData : ScriptableObject
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomly = true;
}
