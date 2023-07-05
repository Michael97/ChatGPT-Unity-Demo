using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GameLogger
{
    private static List<LogEntry> m_logEntries = new List<LogEntry>();
    public static List<LogEntry> LogEntries => m_logEntries;

    private static string m_toChatGptEntries = "";
    public static string ToChatGptEntries => m_toChatGptEntries;

    private static DateTime m_startTime = DateTime.UtcNow;

    public static void LogMessage(string message, LogType logType)
    {
        TimeSpan elapsedTime = DateTime.UtcNow - m_startTime;
        LogEntry entry = new LogEntry(logType, elapsedTime, message);
        m_logEntries.Add(entry);

        if (logType == LogType.ToChatGpt)
        {
            if (string.IsNullOrEmpty(m_toChatGptEntries))
            {
                m_toChatGptEntries = message;
            }
            else
            {
                m_toChatGptEntries += "\n" + message;
            }
        }

        Debug.Log(entry.ToString());
    }

    public static void AppendLogsToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName, true)) // true enables appending mode
        {
            foreach (LogEntry entry in m_logEntries)
            {
                writer.WriteLine(entry.ToString());
            }
        }

        // Clear log entries after writing to file
        m_logEntries.Clear();
    }


    public static void ClearToChatGptEntries()
    {
        m_toChatGptEntries = "";
    }

    public static void SaveFullLog(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (LogEntry entry in m_logEntries)
            {
                writer.WriteLine(entry.ToString());
            }
        }
    }

    public static void SaveTrimmedLog(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (LogEntry entry in m_logEntries)
            {
                if (entry.Type == LogType.FromChatGpt)
                {
                    writer.WriteLine(entry.ToString());
                }
            }
        }
    }

    // Add this method to the GameLogger class
    public static void Reset()
    {
        m_logEntries = new List<LogEntry>();
        m_toChatGptEntries = "";
        m_startTime = DateTime.UtcNow;
    }

}