using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ChatGptAgentSensory : MonoBehaviour
{
    [SerializeField]
    private int visionRadius = 3;

    private List<GameObject> debugSpheres = new List<GameObject>();

    private bool debug = false;

    private Vector2[] directions = new Vector2[]
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

    private string[] directionNames = new string[]
    {
        "N", "NE", "E", "SE", "S", "SW", "W", "NW"
    };

    private Color[] distanceColors = new Color[]
    {
        Color.blue,
        Color.red,
        Color.green,
        Color.yellow // Add more colors if you increase the vision radius
    };

    public virtual string GetVision(Vector2 playerPosition)
    {
        if (debug) RemoveDebugSpheres();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerPosition, visionRadius);
        Dictionary<string, List<string>> tileDataByDistanceAndDirection = new Dictionary<string, List<string>>();

        for (int distance = 1; distance <= visionRadius; distance++)
        {
            for (int d = 0; d < directions.Length; d++)
            {
                string key = $"{distance}:{directionNames[d]}";
                tileDataByDistanceAndDirection[key] = new List<string>();

                for (int i = 0; i < colliders.Length; i++)
                {
                    BaseTile tileData = colliders[i].GetComponent<BaseTile>();

                    if (tileData != null)
                    {
                        JObject tileJsonData = tileData.GetJsonTileData();
                        Vector2 tilePosition = new Vector2((float)tileJsonData["position"]["x"], (float)tileJsonData["position"]["y"]);
                        Vector2 relativePosition = tilePosition - (Vector2)playerPosition + new Vector2(0.5f, 0.5f);

                        if (Mathf.RoundToInt(relativePosition.magnitude) == distance && Vector2.Angle(directions[d], relativePosition) <= 22.5f)
                        {
                            string tileDescription = (string)tileJsonData["description"];
                            if (!tileDataByDistanceAndDirection[key].Contains(tileDescription))
                            {
                                tileDataByDistanceAndDirection[key].Add(tileDescription);
                            }
                            if (debug) SpawnDebugSphere(tilePosition + new Vector2(0.5f, 0.5f), distance - 1);
                        }
                    }
                }
            }
        }

        string currentTile = GetCurrentTile(playerPosition);
        string response = $"Current tile: {currentTile}\n\n";

        for (int distance = 1; distance <= visionRadius; distance++)
        {
            response += $"Within {distance} tiles:\n";
            for (int d = 0; d < directionNames.Length; d++)
            {
                string key = $"{distance}:{directionNames[d]}";
                if (tileDataByDistanceAndDirection[key].Count > 0)
                {
                    response += $"{directionNames[d]}: {string.Join(", ", tileDataByDistanceAndDirection[key])}.\n";
                }
                else
                {
                    response += $"{directionNames[d]}: Nothing.\n";
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
        sphere.GetComponent<Renderer>().material.color = distanceColors[colorIndex];

        debugSpheres.Add(sphere);
    }

    private void RemoveDebugSpheres()
    {
        foreach (GameObject sphere in debugSpheres)
        {
            Destroy(sphere);
        }
        debugSpheres.Clear();
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
