using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MockChatGptAgent : IChatGptAgent
{
    public ChatGptAgentData AgentData
    {        
        get { return m_agentData; }
        set { m_agentData = value; }
    }
    private ChatGptAgentData m_agentData;
    

    public MockChatGptAgent()
    {
        m_agentData = ScriptableObject.CreateInstance<ChatGptAgentData>();
        m_agentData.m_playerName = "MockPlayer";
        m_agentData.m_personalityTypes = new List<PersonalityTypes>
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
        return m_agentData;
    }
}
