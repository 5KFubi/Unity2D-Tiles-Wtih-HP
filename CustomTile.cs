using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Custom Tile", menuName = "Tiles/Custom Tile")]
public class CustomTile : Tile
{
    public CustomTileData tileData;

    // Add a reference to RuleTile
    public RuleTile ruleTile;

    public TileBase GetTileBase()
    {
        // Return either the ruleTile or this CustomTile
        return ruleTile ? ruleTile : this;
    }
}
