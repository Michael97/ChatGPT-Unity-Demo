using System;
using UnityEngine.Networking;

public interface IChatGptAgent
{
    void GetGptResponseAsync(string input, Action<string> onResponse, Action<UnityWebRequest> onError);
    ChatGptAgentData GetChatGptAgentData();
}
