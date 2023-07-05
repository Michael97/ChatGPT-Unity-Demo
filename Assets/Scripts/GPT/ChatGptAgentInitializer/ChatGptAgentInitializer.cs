using OpenAi.Unity.V1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatGptAgentInitializer : MonoBehaviour
{
    public static ChatGptAgentInitializer Instance { get; private set; }

    [Tooltip("The default prefab for the player")]
    [SerializeField] private GameObject m_playerContainerPrefab;

    public List<ChatGptAgentData> ChatGptAgentData
    {        
        get { return m_chatGptAgentData; }
        set { m_chatGptAgentData = value; }
    }

    [Tooltip("Each item represents an agent to be spawned")]
    [SerializeField] private List<ChatGptAgentData> m_chatGptAgentData;

    public List<GameObject> ChatGptAgentList
    {        
        get { return m_chatGptAgentList; }
        set { m_chatGptAgentList = value; }
    }

    [Tooltip("Each chatgpt agent in the scene")] [SerializeField]
    private List<GameObject> m_chatGptAgentList;

    [SerializeField] private GameObject m_chatCompleterPrefab;

    // Define the spawn area limits
    [SerializeField] private Vector2 m_minSpawnPosition = new Vector2(-3, -3);
    [SerializeField] private Vector2 m_maxSpawnPosition = new Vector2(3, 3);
    private HashSet<Vector2> m_usedPositions = new HashSet<Vector2>();

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

    void Start()
    {
        SpawnAgents();
    }

    public void ResetAgents()
    {
        // Destroy all existing agents
        foreach (var agent in m_chatGptAgentList)
        {
            Destroy(agent);
        }
        m_chatGptAgentList.Clear();
        m_usedPositions.Clear();

        // Spawn new agents
        SpawnAgents();
    }

    private void SpawnAgents()
    {
        foreach (var agentData in m_chatGptAgentData)
        {
            // Instantiate the player container
            var playerContainer = Instantiate(m_playerContainerPrefab);

            // Generate a unique position
            Vector2 spawnPosition;
            do
            {
                spawnPosition = new Vector2(
                    Mathf.Round(Random.Range(m_minSpawnPosition.x, m_maxSpawnPosition.x)),
                    Mathf.Round(Random.Range(m_minSpawnPosition.y, m_maxSpawnPosition.y))
                );
            } while (m_usedPositions.Contains(spawnPosition));

            // Add the position to the used positions
            m_usedPositions.Add(spawnPosition);

            // Set the position of the player container
            playerContainer.transform.position = spawnPosition;

            // Instantiate the agent
            var agent = new GameObject(agentData.m_playerName).AddComponent<ChatGptAgent>();
            agent.transform.parent = playerContainer.transform;

            var openAiChatCompleter = Instantiate(m_chatCompleterPrefab, agent.transform);

            agent.Init(playerContainer.GetComponent<Player>(), openAiChatCompleter.GetComponent<OpenAiChatCompleterV1>(), agentData);

            // Add the agent to the list
            m_chatGptAgentList.Add(agent.gameObject);
        }

        ChatGptAgentUIManager.Instance.Setup();
    }
}
