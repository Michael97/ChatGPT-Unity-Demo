public class MockPromptManager : IPromptManager
{
    public string FilePath => "MockFilePath";

    public void GeneratePromptString(IChatGptAgent chatGptAgent, out string prompt)
    {
        ChatGptAgentData agentData = chatGptAgent.GetChatGptAgentData();
        string promptTemplate = "This is a mock prompt with playerName: {playerName}, and personalityTypes: {personalityTypes}.";

        prompt = promptTemplate.Replace("{playerName}", agentData.m_playerName);

        string personalityString = string.Join(", ", agentData.m_personalityTypes);
        prompt = prompt.Replace("{personalityTypes}", personalityString);
    }


}
