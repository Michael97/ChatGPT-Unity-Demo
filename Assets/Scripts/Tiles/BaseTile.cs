using Newtonsoft.Json.Linq;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    [SerializeField]
    protected BaseTileData m_tileDataSO;

    private TileType m_tileType;
    private string m_tileName;
    private int m_movementCost;
    private bool m_isWalkable;
    private bool m_isInteractable;
    private string m_description;
    private Vector2 m_position;

    public BaseTile()
    {
    }

    public BaseTile(TileType tileType, string tileName, int movementCost, bool isWalkable, string description, bool isInteractable)
    {
        m_tileType = tileType;
        m_tileName = tileName;
        m_movementCost = movementCost;
        m_isWalkable = isWalkable;
        m_isInteractable = isInteractable;
        m_description = description;
        m_position = (Vector2)transform.position;
        
    }

    private void Awake()
    {
        if (m_tileDataSO != null)
        {
            m_tileType = m_tileDataSO.m_tileType;
            m_tileName = m_tileDataSO.m_tileName;
            m_movementCost = m_tileDataSO.m_movementCost;
            m_isWalkable = m_tileDataSO.m_isWalkable;
            m_isInteractable = m_tileDataSO.m_isInteractable;
            m_description = m_tileDataSO.m_description;
            m_position = (Vector2)transform.position;
        }
    }

    public TileType TileType
    {
        get { return m_tileType; }
        set { m_tileType = value; }
    }

    public string TileName
    {
        get { return m_tileName; }
        set { m_tileName = value; }
    }

    public int MovementCost
    {
        get { return m_movementCost; }
        set { m_movementCost = value; }
    }

    public bool IsWalkable
    {
        get { return m_isWalkable; }
        set { m_isWalkable = value; }
    }

    public bool IsInteractable
    {
        get { return m_isInteractable; }
        set { m_isInteractable = value; }
    }

    public string Description
    {
        get { return m_description; }
        set { m_description = value; }
    }

    public Vector2 Position
    {
        get { return m_position; }
        set { m_position = (Vector2)transform.position; }
    }

    public virtual JObject GetJsonTileData()
    {
        JObject jsonData = new JObject
        {
            { "tileType", m_tileType.ToString() },
            { "tileName", m_tileName },
            { "description", m_description },
            { "position", new JObject { { "x", m_position.x }, { "y", m_position.y } } }
        };

        return jsonData;
    }

    public BaseTile GetCustomTileData()
    {
        return this;
    }
}
