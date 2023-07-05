using OpenAi.Api.V1;
using OpenAi.Unity.V1;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public interface IDialogueManager
{
    int GetInitialPromptSize();
    void AppendDialogueToFile(string filename);
}

public class DialogueManager : MonoBehaviour, IDialogueManager
{

    [SerializeField] private int m_maxTokenCount = 3696;
    [SerializeField] int m_bufferTokenCount = 100;

    private OpenAiChatCompleterV1 m_chatCompleter;
    private List<MessageV1> m_dialogue => m_chatCompleter.dialogue;


    public int GetTotalTokenCount()
    {
        return CountTokens(m_dialogue);
    }

    public void UpdateLiveData(string data)
    {
        MessageV1 message = new MessageV1();
        message.role = MessageV1.MessageRole.user;
        message.content = data;

        if (m_dialogue.Count > 1)
        {
            m_dialogue[1] = message;
        }
    }

    public void SetInitalPrompt(string prompt)
    {
        MessageV1 message = new MessageV1();
        message.role = MessageV1.MessageRole.user;
        message.content = prompt;

        if (m_dialogue.Count > 0)
        {
            m_dialogue[0] = message;
            
            //Doesn't really matter what we set here, will will overwrite before sending to api
            m_dialogue[1] = message;
        }
        m_dialogue.Add(message);
        m_dialogue.Add(message);
    }

    public void Init(OpenAiChatCompleterV1 chatCompleter)
    {
        this.m_chatCompleter = chatCompleter;

    }

    private int CountTokens(List<MessageV1> dialogue)
    {
        int tokenCount = 0;
        foreach (var message in dialogue)
        {
            tokenCount += message.content.Length / 4;
        }
        return tokenCount;
    }

    private int ToTokens(string message)
    {
        return message.Length / 4;
    }

    public void PurgeExcessMessages(string input)
    {
        const int tokensToPurge = 3000;
        int tokensPurged = 0;
        int indexToPurge = 2;

        GameLogger.LogMessage("Total tokens = " + CountTokens(m_dialogue).ToString(), LogType.Low);

        while (CountTokens(m_dialogue) + ToTokens(input) + m_bufferTokenCount > m_maxTokenCount && tokensPurged < tokensToPurge)
        {
            if (indexToPurge < m_dialogue.Count)
            {
                int messageTokens = m_dialogue[indexToPurge].content.Length / 4;
                if (tokensPurged + messageTokens <= tokensToPurge)
                {
                    tokensPurged += messageTokens;
                    indexToPurge++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                Debug.LogError("Prompt tokens exceed max token count. Please reduce the text length.");
                break;
            }
        }

        GameLogger.LogMessage("Purging " + tokensPurged + " tokens from dialogue.", LogType.Low);

        List<MessageV1> newDialogue = new List<MessageV1> { m_dialogue[0], m_dialogue[1] };
        for (int i = indexToPurge; i < m_dialogue.Count; i++)
        {
            newDialogue.Add(m_dialogue[i]);
        }
        m_dialogue.Clear();
        m_dialogue.AddRange(newDialogue);
    }


    public int GetInitialPromptSize()
    {
        if (m_dialogue.Count > 0)
        {
            return m_dialogue[0].content.Length / 4;
        }
        return 0;
    }

    public void AppendDialogueToFile(string filePath)
    {
        StringBuilder sb = new StringBuilder();

        foreach (MessageV1 message in m_dialogue)
        {
            sb.AppendLine($"[{message.role}]: {message.content}");
        }

        File.AppendAllText(filePath, sb.ToString());
    }
}
