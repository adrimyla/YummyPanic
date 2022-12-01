using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    public void ClearDungeon()
    {
        tilemapVisualizer.Clear();
    }

    public Dungeon GenerateDungeon()
    {
        tilemapVisualizer.Clear();
        return RunProceduralGeneration();
    }

    protected abstract Dungeon RunProceduralGeneration();
}
