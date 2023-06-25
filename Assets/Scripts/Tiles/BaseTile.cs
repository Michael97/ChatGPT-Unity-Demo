using Newtonsoft.Json.Linq;
using UnityEditor.Build.Content;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    [SerializeField]
    public BaseTileData tileDataSO;

    private TileType _tileType;
    private string _tileName;
    private int _movementCost;
    private bool _isWalkable;
    private bool _isInteractable;
    private string _description;
    private Vector2 _position;

    public BaseTile()
    {
    }

    public BaseTile(TileType tileType, string tileName, int movementCost, bool isWalkable, string description, bool isInteractable)
    {
        _tileType = tileType;
        _tileName = tileName;
        _movementCost = movementCost;
        _isWalkable = isWalkable;
        _isInteractable = isInteractable;
        _description = description;
        _position = (Vector2)transform.position;
        
    }

    private void Awake()
    {
        if (tileDataSO != null)
        {
            _tileType = tileDataSO.tileType;
            _tileName = tileDataSO.tileName;
            _movementCost = tileDataSO.movementCost;
            _isWalkable = tileDataSO.isWalkable;
            _isInteractable = tileDataSO.isInteractable;
            _description = tileDataSO.description;
            _position = (Vector2)transform.position;
        }
    }

    public TileType TileType
    {
        get { return _tileType; }
        set { _tileType = value; }
    }

    public string TileName
    {
        get { return _tileName; }
        set { _tileName = value; }
    }

    public int MovementCost
    {
        get { return _movementCost; }
        set { _movementCost = value; }
    }

    public bool IsWalkable
    {
        get { return _isWalkable; }
        set { _isWalkable = value; }
    }

    public bool IsInteractable
    {
        get { return _isInteractable; }
        set { _isInteractable = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public Vector2 Position
    {
        get { return _position; }
        set { _position = (Vector2)transform.position; }
    }

    public virtual JObject GetJsonTileData()
    {
        JObject jsonData = new JObject
        {
            { "tileType", _tileType.ToString() },
            { "tileName", _tileName },
            { "description", _description },
            { "position", new JObject { { "x", _position.x }, { "y", _position.y } } }
        };

        return jsonData;
    }

    public BaseTile GetCustomTileData()
    {
        return this;
    }
}
