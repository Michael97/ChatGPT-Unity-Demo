public interface IChatGptController
{
    void SendMessageToAgent(ChatGptAgent agent, string message);
}
