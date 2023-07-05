using UnityEngine;

public class GameLoggerUpdater : MonoBehaviour
{
    public string LogFileName
    {
        get { return m_logFileName; }
        set { m_logFileName = value; }
    }

    private string m_logFileName;

    private void Awake()
    {
        if (string.IsNullOrEmpty(m_logFileName))
        {
            m_logFileName = DefaultLogFileName();
        }
    }

    private static string DefaultLogFileName()
    {
        string directory = Application.dataPath;
    #if UNITY_EDITOR
        directory = System.IO.Path.GetDirectoryName(directory);
    #endif
        return System.IO.Path.Combine(directory, "game_logs.txt");
    }


    public float UpdateInterval
    {
        get { return m_updateInterval; }
        set { m_updateInterval = value; }
    }

    private float m_updateInterval = 0.1f; // 1/10th of a second

    private float m_nextUpdateTime;

    void Start()
    {
        m_nextUpdateTime = Time.time + UpdateInterval;
    }

    void Update()
    {
        if (Time.time >= m_nextUpdateTime)
        {
            GameLogger.AppendLogsToFile(LogFileName);
            m_nextUpdateTime += UpdateInterval;
        }
    }
}
