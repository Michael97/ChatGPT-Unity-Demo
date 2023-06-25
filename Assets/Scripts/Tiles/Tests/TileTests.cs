using Newtonsoft.Json.Linq;
using NUnit.Framework;
using UnityEngine;

public class TileTests
{
    [Test]
    public void TestMockCustomTileData()
    {
        // Create a mock CustomTileData
        GameObject testObject = new GameObject();
        MockBaseTile mockCustomTileData = testObject.AddComponent<MockBaseTile>();

        // Test GetJsonTileData
        JObject expectedJsonData = new JObject
        {
            { "tileName", "MockTile" },
            { "movementCost", 1 },
            { "isWalkable", true },
            { "isInteractable", false },
            { "position", new JObject { { "x", 1 }, { "y", 1 } } }
        };

        JObject jsonData = mockCustomTileData.GetJsonTileData();
        Assert.AreEqual(expectedJsonData.ToString(), jsonData.ToString());
    }
}
