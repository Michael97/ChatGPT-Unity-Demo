using System;
using OpenAi.Unity.V1;
using UnityEngine;
using UnityEngine.Networking;

public class ChatGptAgent : MonoBehaviour, IChatGptAgent
{
    [SerializeField] private OpenAiChatCompleterV1 openAiChatCompleter;
    [SerializeField] private ChatGptAgentData agentData;


    public Player Player;

    public ChatGptAgentSensory ChatGptAgentSensory { get; set; }
    public ChatGptController ChatGptController { get; set; }
    public DialogueManager DialogueManager { get; set; }
    public PromptManager PromptManager { get; set; }
    public LiveDataManager LiveDataManager { get; set; }

    public ChatGptAgentData AgentData => agentData;
    public OpenAiChatCompleterV1 OpenAiChatCompleter => openAiChatCompleter;

    public Memory memory;

    public void Init(Player player, OpenAiChatCompleterV1 chatCompleter, ChatGptAgentData agentData)
    {
        Player = player;
        this.agentData = agentData;
        openAiChatCompleter = chatCompleter;

        ChatGptAgentSensory = gameObject.AddComponent<ChatGptAgentSensory>();
        ChatGptController = gameObject.AddComponent<ChatGptController>();
        DialogueManager = gameObject.AddComponent<DialogueManager>();
        PromptManager = gameObject.AddComponent<PromptManager>();
        LiveDataManager = gameObject.AddComponent<LiveDataManager>();

        DialogueManager.Init(chatCompleter);
        ChatGptController.Init(this);
        LiveDataManager.Init(this);

        memory = new Memory();
    }

    public void GetGptResponseAsync(string input, Action<string> onResponse, Action<UnityWebRequest> onError)
    {
        DialogueManager.PurgeExcessMessages(input);
        openAiChatCompleter.Complete(input, onResponse, onError);
    }

    public OpenAiChatCompleterV1 OpenAiChatCompleterV1 => openAiChatCompleter;

    public ChatGptAgentData GetChatGptAgentData()
    {
        return AgentData;
    }

}
