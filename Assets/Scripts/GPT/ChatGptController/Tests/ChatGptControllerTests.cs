using NUnit.Framework;

public class ChatGptControllerTests
{
    [Test]
    public void SendMessageToAgentTest()
    {
        // Arrange
        MockChatGptController mockController = new MockChatGptController();

        // Act
        mockController.SendMessageToAgent(null, "");

        // Assert
        Assert.IsTrue(mockController.SendMessageToAgentCalled);
    }
}
