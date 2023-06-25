using Newtonsoft.Json.Linq;
using System.Numerics;

public class MockChatGptAgentSensory : ChatGptAgentSensory
{
    public string GetVision()
    {
        string mockTileData = "MockTile,1,true,false,Vector2(1,1)";
        string response = "GetVision: Following data is formatted as: tileType,tileName,description,position: " + mockTileData;

        return response;
    }
}
