using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;
using Assert = NUnit.Framework.Assert;

/*
public class ResponseParserTests
{
    private GameObject playerControllerObject;
    private IResponseParser responseParser;

    private GameObject mockPlayerControllerObject;
    private IResponseParser responseParserMock;

    private ChatGptAgentManager chatGptAgentManager;

    [SetUp]
    public void Setup()
    {
        //Live Setup
        playerControllerObject = new GameObject();
        chatGptAgentManager.Player = playerControllerObject.AddComponent<Player>();
        chatGptAgentManager = playerControllerObject.AddComponent<ChatGptAgentManager>();
        chatGptAgentManager.ChatGptAgentSensory = playerControllerObject.AddComponent<ChatGptAgentSensory>();

        //Mock Setup
        mockPlayerControllerObject = new GameObject();
        var mockplayerController = mockPlayerControllerObject.AddComponent<PlayerController>();
        var mockPlayerStats = mockPlayerControllerObject.AddComponent<MockPlayerStats>();
        var mockChatGptAgentSensory = mockPlayerControllerObject.AddComponent<MockChatGptAgentSensory>();

        responseParserMock = new MockResponseParser(new MockActionFactory(mockplayerController, mockPlayerStats, mockChatGptAgentSensory));
    }


    [Test]
    public void TestMockGetStats()
    {
        MockGetStatsAction getStatsAction = new MockGetStatsAction(mockPlayerControllerObject.GetComponent<MockPlayerStats>());

        var response = getStatsAction.Execute(new string[] { }, response =>
        {
            Assert.IsTrue(getStatsAction.ExecuteCalled);
            Assert.IsTrue(response.Contains("Health"));
            Assert.IsTrue(response.Contains("Hunger"));
            Assert.IsTrue(response.Contains("Thirst"));
            Assert.IsTrue(response.Contains("Stamina"));
            Assert.IsTrue(response.Contains("Temperature"));
        });
    }

        [Test]
    public void TestParse()
    {
        string response = "{ \"actions\": [\"getStats()\", \"move('north', 1)\"], \"thoughts\": \"I want to check my stats while moving towards the shelter for safety and rest.\", \"follow_up\": \"After getting my current status and scanning my surroundings, I'll decide on my next course of action.\" }";

        var actionParser = new ActionParser(chatGptAgentManager);

        List<IAction> actions = actionParser.Parse(response);

        Assert.IsNotNull(actions);
        Assert.AreEqual(2, actions.Count);
        Assert.IsInstanceOf<GetStatsAction>(actions[0]);
        Assert.IsInstanceOf<GoToAction>(actions[1]);
    }

    [Test]
    public void TestExecute()
    {
        string response = "{\"actions\": [\"getStats()\"], \"thoughts\": \"\", \"follow_up\": \"\" }";

        var actionParser = new ActionParser(chatGptAgentManager);

        List<IAction> actions = actionParser.Parse(response);

        var actionExecutor = new ActionExecutor();

        actionExecutor.Execute(actions[0], null);

        Assert.IsTrue(actions[0].ExecuteCalled);
    }


}
*/