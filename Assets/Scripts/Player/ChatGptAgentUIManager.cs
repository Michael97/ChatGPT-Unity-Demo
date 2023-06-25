using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ChatGptAgentUIManager : MonoBehaviour
{
    public static ChatGptAgentUIManager Instance { get; private set; }

    [SerializeField] private TMP_Dropdown agentDropdown;
    [SerializeField] private TextMeshProUGUI inventoryText;
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private TextMeshProUGUI environmentText;
    [SerializeField] private TextMeshProUGUI positionText;
    [SerializeField] private TextMeshProUGUI memoryText;

    public List<ChatGptAgent> agents;
    public ChatGptAgent selectedAgent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        agents = GetAgents(); // Replace with your method to get a list of players
        UpdatePlayerDropdown();
        agentDropdown.onValueChanged.AddListener(OnAgentSelected);
        //UpdateUI();
    }
    
    public void Setup()
    {
        agents = GetAgents(); // Replace with your method to get a list of players
        UpdatePlayerDropdown();
    }

    private List<ChatGptAgent> GetAgents()
    {
        // Add your method to get a list of all players in the game
        // For example: return GameManager.Instance.GetAllPlayers();

        return FindObjectsOfType<ChatGptAgent>().ToList();
    }

    private void UpdatePlayerDropdown()
    {
        agentDropdown.ClearOptions();
        List<string> playerNames = new List<string>();
        foreach (ChatGptAgent agent in agents)
        {
            playerNames.Add(agent.AgentData.playerName);
        }
        agentDropdown.AddOptions(playerNames);

        if (agents.Count > 0)
        {
            selectedAgent = agents[0];
        }
    }

    private void OnAgentSelected(int index)
    {
        selectedAgent = agents[index];
        //UpdateUI();
    }

    public void UpdateUI(string playerStats, string position, string inventory, string environment, string memory)
    {
        statsText.text = playerStats;
        positionText.text = position;
        inventoryText.text = inventory;
        environmentText.text = environment;
        memoryText.text = memory;
    }

    public void UpdateUI()
    {
        if (selectedAgent == null)
        {
            inventoryText.text = "";
            statsText.text = "";
            return;
        }

        UpdateInventoryText();
        UpdateStatsText();
    }

    private void UpdateInventoryText()
    {
        List<IItem> items = selectedAgent.Player.Inventory.GetInventory();
        inventoryText.text = "Inventory:\n";
        foreach (IItem item in items)
        {
            inventoryText.text += $"{item.Name} x{item.Quantity}\n";
        }
    }

    private void UpdateStatsText()
    {
        statsText.text = "Stats:\n" + selectedAgent.Player.Stats.GetStatsAsString();
    }
}
