using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tool
{
    public static Vector3 Vector2IntToVector3(Vector2Int spawnPos2D)
    {
        return new Vector3((float)spawnPos2D.x + 0.5f, (float)spawnPos2D.y + 0.5f, 0);
    }
}
