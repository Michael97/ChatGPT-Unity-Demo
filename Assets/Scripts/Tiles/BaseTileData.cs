using UnityEngine;

public enum TileType
{
    Obstacle,
    Water,
    Interactable,
    Walkable
}

[CreateAssetMenu(fileName = "CustomTileDataSO", menuName = "TileData/CustomTileDataSO", order = 1)]
public class BaseTileData : ScriptableObject
{
    public TileType tileType;
    public string tileName;
    public int movementCost;
    public bool isWalkable;
    public bool isInteractable;
    public bool isBlocking;
    public string description;
    public Sprite sprite;
}
