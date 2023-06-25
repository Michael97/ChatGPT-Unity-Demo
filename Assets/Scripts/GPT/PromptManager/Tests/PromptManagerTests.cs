using NUnit.Framework;
using System.IO;
using UnityEngine;

public class PromptManagerTests
{
    [Test]
    public void GeneratePromptStringTest()
    {
        // Arrange
        MockPromptManager mockPromptManager = new MockPromptManager();
        MockChatGptAgent mockChatGptAgent = new MockChatGptAgent();

        // Act
        string prompt;
        mockPromptManager.GeneratePromptString(mockChatGptAgent, out prompt);
        
        // Assert
        Assert.AreEqual($"This is a mock prompt with playerName: {mockChatGptAgent.GetChatGptAgentData().playerName}, and personalityTypes: {mockChatGptAgent.GetChatGptAgentData().personalityTypes[0].ToString()}, {mockChatGptAgent.GetChatGptAgentData().personalityTypes[1].ToString()}.", prompt);
    }

}
