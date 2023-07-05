using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

public class ChatGptController : MonoBehaviour, IChatGptController
{
    [SerializeField] private ActionParser m_actionParser;
    [SerializeField] private ActionExecutor m_actionExecutor;

    public void Init(ChatGptAgent agent)
    {
        StartCoroutine(InitFunction(agent, 3f));
    }

    IEnumerator InitFunction(ChatGptAgent agent, float delay)
    {
        yield return new WaitForSeconds(delay);

        m_actionParser = new ActionParser(agent);
        m_actionExecutor = new ActionExecutor();

        agent.PromptManager.GeneratePromptString(agent, out string prompt);

        agent.DialogueManager.SetInitalPrompt(prompt);

        GameLogger.LogMessage($"INIT MESSAGE TO GPT:\n", LogType.High);
        GameLogger.LogMessage(prompt, LogType.ToChatGpt);

        StartCoroutine(UpdateLiveData(agent, () => { }));

        SendMessageToAgent(agent, "Let's Begin");
    }

    private IEnumerator UpdateLiveData(ChatGptAgent agent, Action onFinish)
    {
        var liveData = agent.LiveDataManager.GetLiveData();
        //GameLogger.LogMessage($"LiveData:\n{liveData}", LogType.High);
        Debug.Log("data updated");
        agent.DialogueManager.UpdateLiveData(liveData);

        yield return new WaitForEndOfFrame();
    }

    public void SendMessageToAgent(ChatGptAgent agent, string message)
    {
        StartCoroutine(SendMessageToAgentCoroutine(agent, message));
    }

    public IEnumerator SendMessageToAgentCoroutine(ChatGptAgent agent, string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            //GameLogger.LogMessage("No new messages to send gptagent", LogType.Low);
            GameLogger.LogMessage("Would you like to do anything? Please format your response as suggested earlier.", LogType.ToChatGpt);
            StartCoroutine(CallApiAfterDelay(agent));
            yield break;
        }

        yield return StartCoroutine(UpdateLiveData(agent, () => { }));

        agent.DialogueManager.AppendDialogueToFile("DialogueHistory.txt");

        // Log the message to send to the ChatGptAgent
        GameLogger.LogMessage($"MESSAGE TO BE SENT TO CHAT GPT:\n", LogType.High);
        GameLogger.LogMessage(message, LogType.ToChatGpt);

        agent.GetGptResponseAsync(message,
            response =>
            {
                // Log the message received from the ChatGptAgent
                GameLogger.LogMessage(response, LogType.FromChatGpt);

                // Clear the ToChatGptEntries after sending the messages.
                GameLogger.ClearToChatGptEntries();

                // Parse the response and execute the actions.
                List<IAction> actions = m_actionParser.Parse(response);
                // Define and initialize the actionResults dictionary
                Dictionary<IAction, string> actionResults = new Dictionary<IAction, string>();

                var finishedActionsCount = 0;

                foreach (var action in actions)
                {
                    StartCoroutine(m_actionExecutor.Execute(action, result => {
                        actionResults[action] = result;
                        finishedActionsCount++;

                        if (finishedActionsCount == actions.Count)
                        {
                            // All actions have finished executing
                            // You can now use the results
                            ProcessActionResults(agent, actionResults);
                        }
                    }));
                }
                // GameLogger.LogMessage("Execution result: " + executionResult, LogType.High);
            },
            error =>
            {
                agent.OpenAiChatCompleterV1.dialogue.RemoveAt(agent.OpenAiChatCompleterV1.dialogue.Count-1);
                // This is the onError callback, which will be called if there is an error during the request.
                GameLogger.LogMessage("Error during the request: " + error.error, LogType.High);
                GameLogger.LogMessage("Apologies, there was an error during your request. Please send the request again.", LogType.ToChatGpt);
                // Call the API again after a delay.
                StartCoroutine(UpdateLiveData(agent, () => { }));
                StartCoroutine(CallApiAfterDelay(agent));
            });

    }

    private void ProcessActionResults(ChatGptAgent agent, Dictionary<IAction, string> actionResults)
    {
        StartCoroutine(CallApiAfterDelay(agent));
    }

    private IEnumerator CallApiAfterDelay(ChatGptAgent agent)
    {
        StartCoroutine(UpdateLiveData(agent, () => { }));
        // Set a delay before calling the API again.
        float delay = 5.0f;
        yield return new WaitForSeconds(delay);
        // Call the API with the latest messages.
        SendMessageToAgent(agent, GameLogger.ToChatGptEntries);
    }
}
