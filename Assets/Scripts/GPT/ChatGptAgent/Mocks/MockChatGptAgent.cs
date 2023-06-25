using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MockChatGptAgent : IChatGptAgent
{
    private ChatGptAgentData data;

    public MockChatGptAgent()
    {
        data = ScriptableObject.CreateInstance<ChatGptAgentData>();
        data.playerName = "MockPlayer";
        data.personalityTypes = new List<PersonalityTypes>
        {
            PersonalityTypes.Aggressive,
            PersonalityTypes.Friendly
        };
    }

    public void GetGptResponseAsync(string input, Action<string> onResponse, Action<UnityWebRequest> onError)
    {
        onResponse?.Invoke("Mock response for: " + input);
    }

    public ChatGptAgentData GetChatGptAgentData()
    {
        return data;
    }
}
