using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChatGptAgentData", menuName = "ChatGPT/ChatGptAgentData", order = 1)]
public class ChatGptAgentData : ScriptableObject
{
    [Header("General Information")]
    public string m_playerName;

    [Header("Personality")]
    public List<PersonalityTypes> m_personalityTypes;
}

public enum PersonalityTypes
{
    Aggressive,
    Friendly,
    Cunning,
    Diplomatic,
    Mysterious,
    Analytical
}
