using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TilemapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public CustomTileData defaultTileData;

    private Dictionary<Vector3Int, CustomTile> tilesDictionary = new Dictionary<Vector3Int, CustomTile>();

    void Start()
    {
        InitializeTilemap();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }
    }

    void InitializeTilemap()
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            CustomTile originalTile = tilemap.GetTile(pos) as CustomTile;

            if (originalTile != null)
            {
                CustomTile customTile = Instantiate(originalTile);
                customTile.tileData = Instantiate(originalTile.tileData);
                customTile.tileData.initialHitpoints = defaultTileData.initialHitpoints;

                tilemap.SetTile(pos, customTile);
                tilesDictionary.Add(pos, customTile);
            }
        }
    }

    void HandleMouseClick()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePosition = tilemap.WorldToCell(mouseWorldPos);

        if (tilesDictionary.ContainsKey(tilePosition))
        {
            CustomTile customTile = tilesDictionary[tilePosition];

            if (customTile.tileData.initialHitpoints > 0)
            {
                customTile.tileData.initialHitpoints--;
                tilemap.RefreshTile(tilePosition);

                Debug.Log($"Tile Hit! Remaining Hitpoints: {customTile.tileData.initialHitpoints}");

                if (customTile.tileData.initialHitpoints == 0)
                {
                    tilemap.SetTile(tilePosition, null);
                    tilesDictionary.Remove(tilePosition);
                }
            }
        }
    }
}