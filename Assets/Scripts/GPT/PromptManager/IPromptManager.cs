public interface IPromptManager
{
    string FilePath { get; }
    void GeneratePromptString(IChatGptAgent chatGptAgent, out string prompt);
}
