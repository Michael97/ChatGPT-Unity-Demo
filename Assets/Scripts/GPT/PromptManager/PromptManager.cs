using OpenAi.Api.V1;
using OpenAi.Unity.V1;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class PromptManager : MonoBehaviour, IPromptManager
{
    [SerializeField] private string filePath = "Assets/Prompts/prompt.txt";
    public string FilePath => filePath;
    public string Prompt { get; private set; }

    public void GeneratePromptString(IChatGptAgent chatGptAgent, out string prompt)
    {
        ChatGptAgentData agentData = chatGptAgent.GetChatGptAgentData();
        string promptTemplate = File.ReadAllText(filePath);

        prompt = promptTemplate.Replace("{playerName}", agentData.m_playerName)
                                .Replace("{characterName}", agentData.m_playerName);

        string personalityString = string.Join(", ", agentData.m_personalityTypes.Select(pt => pt.ToString()));
        prompt = prompt.Replace("{personalityTypes}", personalityString);
    }

}
