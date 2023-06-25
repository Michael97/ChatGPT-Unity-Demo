using UnityEditor.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(ChatGptAgentSensory))]

public class ChatGptAgentManager : MonoBehaviour
{
    public GameObject playerContainer;

    public Player Player;

    public ChatGptAgentSensory ChatGptAgentSensory;
    public ChatGptAgent ChatGptAgent;

    public ChatGptAgentManager()
    {
    
    }

    public void Setup()
    {
        Player = playerContainer.GetComponent<Player>();
        ChatGptAgentSensory = gameObject.AddComponent<ChatGptAgentSensory>();
        ChatGptAgent = GetComponentInChildren<ChatGptAgent>();
    }
}
