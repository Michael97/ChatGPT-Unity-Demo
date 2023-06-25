using Newtonsoft.Json.Linq;
using UnityEngine;

public class MockBaseTile : BaseTile
{
    public override JObject GetJsonTileData()
    {
        JObject mockJsonData = new JObject
        {
            { "tileName", "MockTile" },
            { "movementCost", 1 },
            { "isWalkable", true },
            { "isInteractable", false },
            { "position", new JObject { { "x", 1 }, { "y", 1 } } }
        };

        return mockJsonData;
    }
}
