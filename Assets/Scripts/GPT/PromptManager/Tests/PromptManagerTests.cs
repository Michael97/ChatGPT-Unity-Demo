using NUnit.Framework;

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
        Assert.AreEqual($"This is a mock prompt with playerName: {mockChatGptAgent.AgentData.m_playerName}, and personalityTypes: {mockChatGptAgent.AgentData.m_personalityTypes[0].ToString()}, {mockChatGptAgent.AgentData.m_personalityTypes[1].ToString()}.", prompt);
    }

}
