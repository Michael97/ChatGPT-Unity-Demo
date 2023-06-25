public class MockChatGptController : IChatGptController
{
    public bool SendMessageToAgentCalled { get; private set; }

    public void SendMessageToAgent(ChatGptAgent agent, string message)
    {
        SendMessageToAgentCalled = true;
    }
}
