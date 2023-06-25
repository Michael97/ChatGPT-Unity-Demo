using UnityEngine;

public class GameLoggerUpdater : MonoBehaviour
{
    public string logFileName = "Assets/game_logs.txt";
    public float updateInterval = 0.1f; // 1/10th of a second
    private float nextUpdateTime;

    void Start()
    {
        nextUpdateTime = Time.time + updateInterval;
    }

    void Update()
    {
        if (Time.time >= nextUpdateTime)
        {
            GameLogger.AppendLogsToFile(logFileName);
            nextUpdateTime += updateInterval;
        }
    }
}
