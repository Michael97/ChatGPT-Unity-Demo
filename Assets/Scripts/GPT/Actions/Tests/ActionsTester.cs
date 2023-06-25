using NUnit.Framework;
using UnityEngine;
/*
public class ActionsTester
{
    private GameObject playerGameObject;
    private PlayerController playerController;
    private MockPlayerStats mockPlayerStats;
    private MockChatGptAgentSensory mockAgentSensory;
    private MockActionFactory mockActionFactory;

    [SetUp]
    public void Setup()
    {
        // Set up your test environment
        playerGameObject = new GameObject();
        playerController = playerGameObject.AddComponent<PlayerController>();
        mockPlayerStats = playerGameObject.AddComponent<MockPlayerStats>();
        mockAgentSensory = playerGameObject.AddComponent<MockChatGptAgentSensory>();

        mockActionFactory = new MockActionFactory(mockPlayerStats, agent);
    }

    [Test]
    public void TestInteractAction()
    {
        // Arrange
        var mockPlayerInteraction = playerGameObject.AddComponent<MockPlayerInteraction>();
        var interactAction = new InteractAction(mockPlayerInteraction);

        // Act
        interactAction.Execute(new string[] { }, result => { });

        // Assert
        Assert.IsTrue(mockPlayerInteraction.OnInteractPressedCalled);

        // Clean up the test environment
        GameObject.Destroy(playerGameObject);
    }


    [Test]
    public void TestGetStatsAction()
    {
        var getStatsAction = mockActionFactory.GetAction("getStats");

        // Call the GetStats action
        getStatsAction.Execute(null, stats => {
            // Assert the results
            Assert.IsNotNull(stats);
            Assert.IsTrue(stats.Contains("Health: "));
            Assert.IsTrue(stats.Contains("Hunger: "));
            Assert.IsTrue(stats.Contains("Thirst: "));
            Assert.IsTrue(stats.Contains("Stamina: "));
            Assert.IsTrue(stats.Contains("Temperature: "));
        });

        // Clean up the test environment
        GameObject.Destroy(playerGameObject);
    }


    [Test]
    public void TestGoToAction()
    {
        var goToAction = mockActionFactory.GetAction("goTo");

        // Call the GoTo action
        goToAction.Execute(new string[] { "up", "1" }, result => {
            // Assert the results
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("Successfully moved"));
        });

        // Clean up the test environment
        GameObject.Destroy(playerGameObject);
    }


    [Test]
    public void TestGetVisionAction()
    {
        var getVisionAction = mockActionFactory.GetAction("getVision");

        // Call the GetVision action
        getVisionAction.Execute(null, visionData => {
            // Assert the results
            Assert.IsNotNull(visionData);
            Assert.IsTrue(visionData == "GetVision: Following data is formatted as: tileType,tileName,description,position: MockTile,1,true,false,Vector2(1,1)");
        });

        // Clean up the test environment
        GameObject.Destroy(playerGameObject);
    }
}
*/