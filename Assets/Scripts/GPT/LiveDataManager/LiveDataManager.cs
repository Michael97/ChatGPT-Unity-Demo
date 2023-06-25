using System;
using System.Collections.Generic;
using UnityEngine;

public interface ILiveData
{
    string GetLiveData();
}

public interface ILiveDataUpdater
{
    void UpdateLiveData();
}

public class LiveDataManager : MonoBehaviour, ILiveData, ILiveDataUpdater
{
    [SerializeField] private ActionExecutor actionExecutor;
    [SerializeField] private ActionParser actionParser;

    private string playerStats;
    private string position;
    private string inventory;
    private string environment;
    private string memory;

    public void Init(ChatGptAgent agent)
    {
        actionExecutor = new ActionExecutor();
        actionParser = new ActionParser(agent);
    }

    void Start()
    {
        UpdateLiveData();
    }

    private void SetLiveData(string playerStats, string position, string inventory, string environment, string memory)
    {
        this.playerStats = playerStats;
        this.position = position;
        this.inventory = inventory;
        this.environment = environment;
        this.memory = memory;
    }

    public string GetLiveData()
    {
        UpdateLiveData();
        return $"ESSENTIAL - Below is live data from the world, after each message, refer here to update your knowledge of the world:\nYour Position:\n{position}\n\nYour Stats:\n{playerStats}\n\nInventory:\n{inventory}\n\nEnvironment:\n{environment}";
    }

    public void UpdateLiveData()
    {
        var stringActions = "Actions:\n- adminGetStats()\n- adminGetPlayerPosition()\n- adminGetInventory()\n- adminGetVision()\n- adminGetMemoryKeys()";

        // Parse the response and execute the actions.
        List<IAction> actions = actionParser.Parse(stringActions);
        // Define and initialize the actionResults dictionary
        Dictionary<IAction, string> actionResults = new Dictionary<IAction, string>();

        var finishedActionsCount = 0;

        foreach (var action in actions)
        {
            StartCoroutine(actionExecutor.Execute(action, result => {
                actionResults[action] = result;
                finishedActionsCount++;

                if (finishedActionsCount == actions.Count)
                {
                    // All actions have finished executing
                    // You can now use the results
                    ProcessActionResults(actionResults);
                }
            }));
        }
    }

    void ProcessActionResults(Dictionary<IAction, string> actionResults)
    {
        foreach (var actionResult in actionResults)
        {
            if (actionResult.Key is GetStatsAction)
            {
                playerStats = actionResult.Value;
            }
            else if (actionResult.Key is GetPlayerPositionAction)
            {
                position = actionResult.Value;
            }
            else if (actionResult.Key is GetInventoryAction)
            {
                inventory = actionResult.Value;
            }
            else if (actionResult.Key is GetVisionAction)
            {
                environment = actionResult.Value;
            }
            else if (actionResult.Key is GetAllMemoryAction)
            {
                memory = actionResult.Value;
            }
        }

        // Update the live data
        SetLiveData(playerStats, position, inventory, environment, memory);

        ChatGptAgentUIManager.Instance.UpdateUI(playerStats, position, inventory, environment, memory);
    }
}
