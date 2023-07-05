using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ChatGptAgentSensory : MonoBehaviour
{
    public int VisionRadius 
    {        
        get { return m_visionRadius; }
        set { m_visionRadius = value; }
    }

    [SerializeField] private int m_visionRadius = 3;

    public bool DebugMode 
    {        
        get { return m_debugMode; }
        set { m_debugMode = value; }
    }
    
    [SerializeField] private bool m_debugMode = false;

    private List<GameObject> m_debugSpheres = new List<GameObject>();

    private Vector2[] m_directions = new Vector2[]
    {
        new Vector2(0, 1),
        new Vector2(1, 1),
        new Vector2(1, 0),
        new Vector2(1, -1),
        new Vector2(0, -1),
        new Vector2(-1, -1),
        new Vector2(-1, 0),
        new Vector2(-1, 1)
    };

    private string[] m_directionNames = new string[]
    {
        "N", "NE", "E", "SE", "S", "SW", "W", "NW"
    };

    private Color[] m_distanceColors = new Color[]
    {
        Color.blue,
        Color.red,
        Color.green,
        Color.yellow
    };

    public virtual string GetVision(Vector2 playerPosition)
    {
        if (m_debugMode) RemoveDebugSpheres();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerPosition, m_visionRadius);
        Dictionary<string, List<string>> tileDataByDistanceAndDirection = new Dictionary<string, List<string>>();

        for (int distance = 1; distance <= m_visionRadius; distance++)
        {
            for (int d = 0; d < m_directions.Length; d++)
            {
                string key = $"{distance}:{m_directionNames[d]}";
                tileDataByDistanceAndDirection[key] = new List<string>();

                for (int i = 0; i < colliders.Length; i++)
                {
                    BaseTile tileData = colliders[i].GetComponent<BaseTile>();

                    if (tileData != null)
                    {
                        JObject tileJsonData = tileData.GetJsonTileData();
                        Vector2 tilePosition = new Vector2((float)tileJsonData["position"]["x"], (float)tileJsonData["position"]["y"]);
                        Vector2 relativePosition = tilePosition - (Vector2)playerPosition + new Vector2(0.5f, 0.5f);

                        if (Mathf.RoundToInt(relativePosition.magnitude) == distance && Vector2.Angle(m_directions[d], relativePosition) <= 22.5f)
                        {
                            string tileDescription = (string)tileJsonData["description"];
                            if (!tileDataByDistanceAndDirection[key].Contains(tileDescription))
                            {
                                tileDataByDistanceAndDirection[key].Add(tileDescription);
                            }
                            if (m_debugMode) SpawnDebugSphere(tilePosition + new Vector2(0.5f, 0.5f), distance - 1);
                        }
                    }
                }
            }
        }

        string currentTile = GetCurrentTile(playerPosition);
        string response = $"Current tile: {currentTile}\n\n";

        for (int distance = 1; distance <= m_visionRadius; distance++)
        {
            response += $"Within {distance} tiles:\n";
            for (int d = 0; d < m_directionNames.Length; d++)
            {
                string key = $"{distance}:{m_directionNames[d]}";
                if (tileDataByDistanceAndDirection[key].Count > 0)
                {
                    response += $"{m_directionNames[d]}: {string.Join(", ", tileDataByDistanceAndDirection[key])}.\n";
                }
                else
                {
                    response += $"{m_directionNames[d]}: Nothing.\n";
                }
            }
        }
        return response;
    }

    private void SpawnDebugSphere(Vector2 tilePosition, int colorIndex)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(tilePosition.x, tilePosition.y, -1);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.GetComponent<Renderer>().material.color = m_distanceColors[colorIndex];

        m_debugSpheres.Add(sphere);
    }

    private void RemoveDebugSpheres()
    {
        foreach (GameObject sphere in m_debugSpheres)
        {
            Destroy(sphere);
        }
        m_debugSpheres.Clear();
    }

    private string GetCurrentTile(Vector2 playerPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerPosition, 0.1f);

        foreach (Collider2D collider in colliders)
        {
            BaseTile tileData = collider.GetComponent<BaseTile>();
            if (tileData != null)
            {
                JObject tileJsonData = tileData.GetJsonTileData();
                return (string)tileJsonData["description"];
            }
        }
        return "Unknown";
    }
}
