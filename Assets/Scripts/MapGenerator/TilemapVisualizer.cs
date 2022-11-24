using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap, kitchenTileMap;
    [SerializeField]
    private TileBase floorTile, wallTile, kitchenTile;

    public void DisplayDungeon(IEnumerable<Vector2Int> floorPositions, IEnumerable<Vector2Int> wallPositions, Kitchen kitchen)
    {
        //Display dungeon floor
        PaintFloorTiles(floorPositions);

        //Displaying walls
        PaintWallTiles(wallPositions);

        //Displaying kitchen floor
        PaintKitchenFloorTiles(kitchen.floorPos);

        //Displaying kitchen walls
        PaintWallTiles(kitchen.wallPos);
    }

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    public void PaintKitchenFloorTiles(IEnumerable<Vector2Int> kitchenPositions)
    {
        PaintTiles(kitchenPositions, kitchenTileMap, kitchenTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase tile)
    {
        foreach(var pos in positions)
        {
            PaintSingleTile(tileMap, tile, pos);
        }
    }

    private void PaintWallTiles(IEnumerable<Vector2Int> positions)
    {
        foreach (var pos in positions)
        {
            PaintSingleBasicWall(pos);
        }
    }

    private void PaintSingleTile(Tilemap tileMap, TileBase tile, Vector2Int pos)
    {
        var tilePosition = tileMap.WorldToCell((Vector3Int)pos);
        tileMap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        kitchenTileMap.ClearAllTiles();
    }

    internal void PaintSingleBasicWall(Vector2Int position)
    {
       PaintSingleTile(wallTilemap, wallTile, position);
    }
}
