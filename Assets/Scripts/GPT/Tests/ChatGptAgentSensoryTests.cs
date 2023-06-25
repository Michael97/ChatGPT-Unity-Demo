using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Newtonsoft.Json.Linq;

public class ChatGptAgentSensoryTests
{
    [Test]
    public void GetVisionTest()
    {
        // Create a test object and attach the mock component
        GameObject testObject = new GameObject();
        MockChatGptAgentSensory mockSensory = testObject.AddComponent<MockChatGptAgentSensory>();

        // Call the GetVision method and check the returned data
        string visionData = mockSensory.GetVision();
        string expectedResponse = "GetVision: Following data is formatted as: tileType,tileName,description,position: MockTile,1,true,false,Vector2(1,1)";

        Assert.AreEqual(expectedResponse, visionData);

        // Clean up
        Object.Destroy(testObject);
    }
}
