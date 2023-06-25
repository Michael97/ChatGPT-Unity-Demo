using OpenAi.Api.V1;
using OpenAi.Unity.V1;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor.VersionControl;
using UnityEngine;

public interface IDialogueManager
{
    int GetInitialPromptSize();
    void AppendDialogueToFile(string filename);
}

public class DialogueManager : MonoBehaviour, IDialogueManager
{

    public int maxTokenCount = 3696;
    public int bufferTokenCount = 100;

    private OpenAiChatCompleterV1 chatCompleter;
    private List<MessageV1> dialogue => chatCompleter.dialogue;


    public int GetTotalTokenCount()
    {
        return CountTokens(dialogue);
    }

    public void UpdateLiveData(string data)
    {
        MessageV1 message = new MessageV1();
        message.role = MessageV1.MessageRole.user;
        message.content = data;

        if (dialogue.Count > 1)
        {
            dialogue[1] = message;
        }
    }

    public void SetInitalPrompt(string prompt)
    {
        MessageV1 message = new MessageV1();
        message.role = MessageV1.MessageRole.user;
        message.content = prompt;

        if (dialogue.Count > 0)
        {
            dialogue[0] = message;
            
            //Doesn't really matter what we set here, will will overwrite before sending to api
            dialogue[1] = message;
        }
        dialogue.Add(message);
        dialogue.Add(message);
    }

    public void Init(OpenAiChatCompleterV1 chatCompleter)
    {
        this.chatCompleter = chatCompleter;

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

        GameLogger.LogMessage("Total tokens = " + CountTokens(dialogue).ToString(), LogType.Low);

        while (CountTokens(dialogue) + ToTokens(input) + bufferTokenCount > maxTokenCount && tokensPurged < tokensToPurge)
        {
            if (indexToPurge < dialogue.Count)
            {
                int messageTokens = dialogue[indexToPurge].content.Length / 4;
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

        List<MessageV1> newDialogue = new List<MessageV1> { dialogue[0], dialogue[1] };
        for (int i = indexToPurge; i < dialogue.Count; i++)
        {
            newDialogue.Add(dialogue[i]);
        }
        dialogue.Clear();
        dialogue.AddRange(newDialogue);
    }


    public int GetInitialPromptSize()
    {
        if (dialogue.Count > 0)
        {
            return dialogue[0].content.Length / 4;
        }
        return 0;
    }

    public void AppendDialogueToFile(string filePath)
    {
        StringBuilder sb = new StringBuilder();

        foreach (MessageV1 message in dialogue)
        {
            sb.AppendLine($"[{message.role}]: {message.content}");
        }

        File.AppendAllText(filePath, sb.ToString());
    }


    // Add other methods and logic specific to your project here
}
