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
    public TileType m_tileType;
    public string m_tileName;
    public int m_movementCost;
    public bool m_isWalkable;
    public bool m_isInteractable;
    public bool m_isBlocking;
    public string m_description;
    public Sprite m_sprite;
}
