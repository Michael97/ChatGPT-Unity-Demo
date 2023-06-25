using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GameLogger
{
    private static List<LogEntry> logEntries = new List<LogEntry>();
    public static List<LogEntry> LogEntries => logEntries;

    private static string toChatGptEntries = "";
    public static string ToChatGptEntries => toChatGptEntries;

    private static DateTime startTime = DateTime.UtcNow;

    public static void LogMessage(string message, LogType logType)
    {
        TimeSpan elapsedTime = DateTime.UtcNow - startTime;
        LogEntry entry = new LogEntry(logType, elapsedTime, message);
        logEntries.Add(entry);

        if (logType == LogType.ToChatGpt)
        {
            if (string.IsNullOrEmpty(toChatGptEntries))
            {
                toChatGptEntries = message;
            }
            else
            {
                toChatGptEntries += "\n" + message;
            }
        }

        Debug.Log(entry.ToString());
    }

    public static void AppendLogsToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName, true)) // true enables appending mode
        {
            foreach (LogEntry entry in logEntries)
            {
                writer.WriteLine(entry.ToString());
            }
        }

        // Clear log entries after writing to file
        logEntries.Clear();
    }


    public static void ClearToChatGptEntries()
    {
        toChatGptEntries = "";
    }

    public static void SaveFullLog(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (LogEntry entry in logEntries)
            {
                writer.WriteLine(entry.ToString());
            }
        }
    }

    public static void SaveTrimmedLog(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (LogEntry entry in logEntries)
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
        logEntries = new List<LogEntry>();
        toChatGptEntries = "";
        startTime = DateTime.UtcNow;
    }

}