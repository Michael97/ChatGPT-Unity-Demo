using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class ResponseParser : MonoBehaviour, IResponseParser
{
    public static ResponseParser Instance { get; private set; }

    [SerializeField] private ChatGptAgent m_agent;

    protected IActionFactory m_actionFactory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            m_actionFactory = new ActionFactory(m_agent);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static string RemoveAllEscapeCharacters(string input)
    {
        string output = input;
        bool hasChanges;
        int loopCount = 0;

        do
        {
            string newOutput = output.Replace("\\n", "").Replace("\\\"", "\"").Replace("\\\\", "\\").Replace("\\", "");
            hasChanges = newOutput.Length != output.Length;
            output = newOutput;
            loopCount++;
        }
        while (hasChanges);

        if (loopCount > 2)
        {
            GameLogger.LogMessage($"Response Format is of poor quality. We had to parse it {loopCount} times. Please keep to the example response.", LogType.ToChatGpt);
        }

        return output;
    }

    public virtual List<IAction> Parse(string response)
    {
        List<IAction> actions = new List<IAction>();

        // Remove all escape characters
        string unescapedResponse = RemoveAllEscapeCharacters(response);

        // Use regex to match the Actions section
        Match actionsMatch = Regex.Match(unescapedResponse, @"Actions:\s*(?:-\s*(\w+\([^)]*\))\s*)+", RegexOptions.Multiline);

        if (actionsMatch.Success)
        {
            // Get all action strings
            MatchCollection actionStrings = Regex.Matches(unescapedResponse, @"-\s*(\w+\([^)]*\))", RegexOptions.Multiline);

            foreach (Match actionMatch in actionStrings)
            {
                string actionString = actionMatch.Groups[1].Value;
                string actionName = GetActionNameFromActionString(actionString);

                if (actionName == null)
                {
                    GameLogger.LogMessage($"{actionString} Invalid request, please keep format to the example given", LogType.ToChatGpt);
                    return actions;
                }

                IAction action = m_actionFactory.GetAction(actionName);

                if (action != null)
                {
                    string[] parameters = GetParametersFromActionString(actionString);
                    action.Parameters = parameters;
                    actions.Add(action);
                }
            }
        }
        else
        {
            GameLogger.LogMessage("Invalid response, please keep format to the example given", LogType.ToChatGpt);
        }

        return actions;
    }

    public virtual IEnumerator Execute(List<IAction> actions, Action callback, Dictionary<IAction, string> actionResults)
    {
        int finishedCoroutinesCount = 0;

        for (int i = 0; i < actions.Count; i++)
        {
            Debug.LogError($"Action: {actions[i]}");
            StartCoroutine(actions[i].Execute(actions[i].Parameters, result => {
                GameLogger.LogMessage(result, LogType.Low);
                actionResults[actions[i]] = result;
                finishedCoroutinesCount++;
            }));
        }

        // Wait for all coroutines to finish
        yield return new WaitUntil(() => finishedCoroutinesCount == actions.Count);

        // Invoke the callback after executing all the actions
        callback?.Invoke();
    }


    public static string GetActionNameFromActionString(string actionString)
    {
        int openParenthesisIndex = actionString.IndexOf('(');
        string actionName = actionString.Substring(0, openParenthesisIndex);
        return actionName.Replace("'", "");
    }


    public static string[] GetParametersFromActionString(string actionString)
    {
        int openParenthesisIndex = actionString.IndexOf('(');
        int closeParenthesisIndex = actionString.IndexOf(')');
        string parametersString = actionString.Substring(openParenthesisIndex + 1, closeParenthesisIndex - openParenthesisIndex - 1);

        if (parametersString.Length > 0)
        {
            string[] parameters = parametersString.Split(',');
            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = parameters[i].Trim('\'');
            }
            return parameters;
        }
        else
        {
            return new string[0];
        }
    }
}