using UnityEditor.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(ChatGptAgentSensory))]

public class ChatGptAgentManager : MonoBehaviour
{
    public GameObject PlayerContainer
    {        
        get { return m_playerContainer; }
        set { m_playerContainer = value; }
    }

    private GameObject m_playerContainer;

    public Player Player
    {        
        get { return m_player; }
        set { m_player = value; }
    }

    private Player m_player;

    public ChatGptAgentSensory ChatGptAgentSensory
    {        
        get { return m_chatGptAgentSensory; }
        set { m_chatGptAgentSensory = value; }
    }

    private ChatGptAgentSensory m_chatGptAgentSensory;

    public ChatGptAgent ChatGptAgent
    {        
        get { return ChatGptAgent; }
        set { ChatGptAgent = value; }
    }

    private ChatGptAgent m_chatGptAgent;

    public ChatGptAgentManager()
    {
    
    }

    public void Setup()
    {
        m_player = m_playerContainer.GetComponent<Player>();
        m_chatGptAgentSensory = gameObject.AddComponent<ChatGptAgentSensory>();
        m_chatGptAgent = GetComponentInChildren<ChatGptAgent>();
    }
}
