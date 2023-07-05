using System;
using OpenAi.Unity.V1;
using UnityEngine;
using UnityEngine.Networking;

public class ChatGptAgent : MonoBehaviour, IChatGptAgent
{


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

    public ChatGptController ChatGptController
    {        
        get { return m_chatGptController; }
        set { m_chatGptController = value; }
    }

    private ChatGptController m_chatGptController;

    public DialogueManager DialogueManager
    {        
        get { return m_dialogueManager; }
        set { m_dialogueManager = value; }
    }

    private DialogueManager m_dialogueManager;

    public PromptManager PromptManager
    {        
        get { return m_promptManager; }
        set { m_promptManager = value; }
    }

    private PromptManager m_promptManager;

    public LiveDataManager LiveDataManager
    {        
        get { return m_liveDataManager; }
        set { m_liveDataManager = value; }
    }

    private LiveDataManager m_liveDataManager;

    public ChatGptAgentData AgentData
    {        
        get { return m_chatGptAgentData; }
        set { m_chatGptAgentData = value; }
    }

    [SerializeField] private ChatGptAgentData m_chatGptAgentData;

    public OpenAiChatCompleterV1 OpenAiChatCompleter
    {        
        get { return m_openAiChatCompleter; }
        set { m_openAiChatCompleter = value; }
    }

    [SerializeField] private OpenAiChatCompleterV1 m_openAiChatCompleter;

    public Memory memory
    {        
        get { return m_memory; }
        set { m_memory = value; }
    }

    private Memory m_memory;

    public void Init(Player player, OpenAiChatCompleterV1 chatCompleter, ChatGptAgentData agentData)
    {
        m_player = player;
        m_chatGptAgentData = agentData;
        m_openAiChatCompleter = chatCompleter;

        m_chatGptAgentSensory = gameObject.AddComponent<ChatGptAgentSensory>();
        m_chatGptController = gameObject.AddComponent<ChatGptController>();
        m_dialogueManager = gameObject.AddComponent<DialogueManager>();
        m_promptManager = gameObject.AddComponent<PromptManager>();
        m_liveDataManager = gameObject.AddComponent<LiveDataManager>();

        m_dialogueManager.Init(chatCompleter);
        m_chatGptController.Init(this);
        m_liveDataManager.Init(this);

        m_memory = new Memory();
    }

    public void GetGptResponseAsync(string input, Action<string> onResponse, Action<UnityWebRequest> onError)
    {
        m_dialogueManager.PurgeExcessMessages(input);
        m_openAiChatCompleter.Complete(input, onResponse, onError);
    }

    public OpenAiChatCompleterV1 OpenAiChatCompleterV1 => m_openAiChatCompleter;

    public ChatGptAgentData GetChatGptAgentData()
    {
        return AgentData;
    }

}
