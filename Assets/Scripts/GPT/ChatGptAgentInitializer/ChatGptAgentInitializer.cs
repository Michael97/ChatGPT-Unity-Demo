using OpenAi.Unity.V1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatGptAgentInitializer : MonoBehaviour
{
    public static ChatGptAgentInitializer Instance { get; private set; }

    [Tooltip("The default prefab for the player")]
    public GameObject m_playerContainerPrefab;

    [Tooltip("Each item represents an agent to be spawned")]
    public List<ChatGptAgentData> m_chatGptAgentData;

    [Tooltip("Each chatgpt agent in the scene")]
    public List<GameObject> m_chatGptAgentList;

    public GameObject m_chatCompleterPrefab;

    // Define the spawn area limits
    public Vector2 minSpawnPosition = new Vector2(-3, -3);
    public Vector2 maxSpawnPosition = new Vector2(3, 3);

    private HashSet<Vector2> usedPositions = new HashSet<Vector2>();

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
        usedPositions.Clear();

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
                    Mathf.Round(Random.Range(minSpawnPosition.x, maxSpawnPosition.x)),
                    Mathf.Round(Random.Range(minSpawnPosition.y, maxSpawnPosition.y))
                );
            } while (usedPositions.Contains(spawnPosition));

            // Add the position to the used positions
            usedPositions.Add(spawnPosition);

            // Set the position of the player container
            playerContainer.transform.position = spawnPosition;

            // Instantiate the agent
            var agent = new GameObject(agentData.playerName).AddComponent<ChatGptAgent>();
            agent.transform.parent = playerContainer.transform;

            var openAiChatCompleter = Instantiate(m_chatCompleterPrefab, agent.transform);

            agent.Init(playerContainer.GetComponent<Player>(), openAiChatCompleter.GetComponent<OpenAiChatCompleterV1>(), agentData);

            // Add the agent to the list
            m_chatGptAgentList.Add(agent.gameObject);
        }

        ChatGptAgentUIManager.Instance.Setup();
    }
}
