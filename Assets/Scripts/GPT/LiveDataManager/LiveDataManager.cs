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
    [SerializeField] private ActionExecutor m_actionExecutor;
    [SerializeField] private ActionParser m_actionParser;

    private string m_playerStats;
    private string m_position;
    private string m_inventory;
    private string m_environment;
    private string m_memory;

    public void Init(ChatGptAgent agent)
    {
        m_actionExecutor = new ActionExecutor();
        m_actionParser = new ActionParser(agent);
    }

    void Start()
    {
        UpdateLiveData();
    }

    private void SetLiveData(string playerStats, string position, string inventory, string environment, string memory)
    {
        m_playerStats = playerStats;
        m_position = position;
        m_inventory = inventory;
        m_environment = environment;
        m_memory = memory;
    }

    public string GetLiveData()
    {
        UpdateLiveData();
        return $"ESSENTIAL - Below is live data from the world, after each message, refer here to update your knowledge of the world:\nYour Position:\n{m_position}\n\nYour Stats:\n{m_playerStats}\n\nInventory:\n{m_inventory}\n\nEnvironment:\n{m_environment}";
    }

    public void UpdateLiveData()
    {
        var stringActions = "Actions:\n- adminGetStats()\n- adminGetPlayerPosition()\n- adminGetInventory()\n- adminGetVision()\n- adminGetMemoryKeys()";

        // Parse the response and execute the actions.
        List<IAction> actions = m_actionParser.Parse(stringActions);
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
                m_playerStats = actionResult.Value;
            }
            else if (actionResult.Key is GetPlayerPositionAction)
            {
                m_position = actionResult.Value;
            }
            else if (actionResult.Key is GetInventoryAction)
            {
                m_inventory = actionResult.Value;
            }
            else if (actionResult.Key is GetVisionAction)
            {
                m_environment = actionResult.Value;
            }
            else if (actionResult.Key is GetAllMemoryAction)
            {
                m_memory = actionResult.Value;
            }
        }

        // Update the live data
        SetLiveData(m_playerStats, m_position, m_inventory, m_environment, m_memory);

        ChatGptAgentUIManager.Instance.UpdateUI(m_playerStats, m_position, m_inventory, m_environment, m_memory);
    }
}
