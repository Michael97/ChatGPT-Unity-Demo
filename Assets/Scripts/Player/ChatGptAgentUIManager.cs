using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ChatGptAgentUIManager : MonoBehaviour
{
    public static ChatGptAgentUIManager Instance { get; private set; }

    [SerializeField] private TMP_Dropdown m_agentDropdown;
    [SerializeField] private TextMeshProUGUI m_inventoryText;
    [SerializeField] private TextMeshProUGUI m_statsText;
    [SerializeField] private TextMeshProUGUI m_environmentText;
    [SerializeField] private TextMeshProUGUI m_positionText;
    [SerializeField] private TextMeshProUGUI m_memoryText;

    [SerializeField] private List<ChatGptAgent> m_agents;
    [SerializeField] private ChatGptAgent m_selectedAgent;

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
        m_agents = GetAgents();
        UpdatePlayerDropdown();
        m_agentDropdown.onValueChanged.AddListener(OnAgentSelected);
        //UpdateUI();
    }
    
    public void Setup()
    {
        m_agents = GetAgents();
        UpdatePlayerDropdown();
    }

    private List<ChatGptAgent> GetAgents()
    {
        return FindObjectsOfType<ChatGptAgent>().ToList();
    }

    private void UpdatePlayerDropdown()
    {
        m_agentDropdown.ClearOptions();
        List<string> playerNames = new List<string>();
        foreach (ChatGptAgent agent in m_agents)
        {
            playerNames.Add(agent.AgentData.m_playerName);
        }
        m_agentDropdown.AddOptions(playerNames);

        if (m_agents.Count > 0)
        {
            m_selectedAgent = m_agents[0];
        }
    }

    private void OnAgentSelected(int index)
    {
        m_selectedAgent = m_agents[index];
        //UpdateUI();
    }

    public void UpdateUI(string playerStats, string position, string inventory, string environment, string memory)
    {
        m_statsText.text = playerStats;
        m_positionText.text = position;
        m_inventoryText.text = inventory;
        m_environmentText.text = environment;
        m_memoryText.text = memory;
    }

    public void UpdateUI()
    {
        if (m_selectedAgent == null)
        {
            m_inventoryText.text = "";
            m_statsText.text = "";
            return;
        }

        UpdateInventoryText();
        UpdateStatsText();
    }

    private void UpdateInventoryText()
    {
        List<IItem> items = m_selectedAgent.Player.Inventory.GetInventory();
        m_inventoryText.text = "Inventory:\n";
        foreach (IItem item in items)
        {
            m_inventoryText.text += $"{item.Name} x{item.Quantity}\n";
        }
    }

    private void UpdateStatsText()
    {
        m_statsText.text = "Stats:\n" + m_selectedAgent.Player.Stats.GetStatsAsString();
    }
}
