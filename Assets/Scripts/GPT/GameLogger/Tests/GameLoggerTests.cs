using System.IO;
using NUnit.Framework;
using UnityEngine;

public class GameLoggerTests
{
    [SetUp]
    public void SetUp()
    {
        GameLogger.Reset();
    }

    [Test]
    public void LogMessage_AddsEntryToList()
    {
        GameLogger.LogMessage("Test message", LogType.FromChatGpt);
        Assert.AreEqual(1, GameLogger.LogEntries.Count);
        Assert.AreEqual("Test message", GameLogger.LogEntries[0].Message);
        Assert.AreEqual(LogType.FromChatGpt, GameLogger.LogEntries[0].Type);
    }

    [Test]
    public void SaveFullLog_CreatesLogFile()
    {
        GameLogger.LogMessage("Test message", LogType.FromChatGpt);

        string fileName = Application.dataPath + "/Logs/full_log.txt";
        GameLogger.SaveFullLog(fileName);

        Assert.IsTrue(File.Exists(fileName));
        File.Delete(fileName);
    }

    [Test]
    public void SaveTrimmedLog_CreatesLogFile()
    {
        GameLogger.LogMessage("Test message", LogType.FromChatGpt);

        string fileName = Application.dataPath + "/Logs/trimmed_log.txt";
        GameLogger.SaveTrimmedLog(fileName);

        Assert.IsTrue(File.Exists(fileName));
        File.Delete(fileName);
    }

    [Test]
    public void SaveFullLog_RecordsAllEntries()
    {
        GameLogger.LogMessage("Message 1", LogType.ToChatGpt);
        GameLogger.LogMessage("Message 2", LogType.FromChatGpt);
        GameLogger.LogMessage("Message 3", LogType.FunctionRequest);
        GameLogger.LogMessage("Message 4", LogType.FunctionExecution);

        string fileName = Application.dataPath + "/Logs/full_log_test.txt";
        GameLogger.SaveFullLog(fileName);

        string[] lines = File.ReadAllLines(fileName);
        Assert.AreEqual(4, lines.Length);
        File.Delete(fileName);
    }

    [Test]
    public void SaveTrimmedLog_RecordsOnlyChatGptResponses()
    {
        GameLogger.LogMessage("Message 1", LogType.ToChatGpt);
        GameLogger.LogMessage("Message 2", LogType.FromChatGpt);
        GameLogger.LogMessage("Message 3", LogType.FunctionRequest);
        GameLogger.LogMessage("Message 4", LogType.FunctionExecution);

        string fileName = Application.dataPath + "/Logs/trimmed_log_test.txt";
        GameLogger.SaveTrimmedLog(fileName);

        string[] lines = File.ReadAllLines(fileName);
        Assert.AreEqual(1, lines.Length);
        Assert.IsTrue(lines[0].Contains("Message 2"));
        File.Delete(fileName);
    }

    [Test]
    public void ClearToChatGptEntries_ResetsToChatGptEntries()
    {
        GameLogger.LogMessage("Test message", LogType.ToChatGpt);
        Assert.AreEqual(1, GameLogger.LogEntries.Count);

        GameLogger.ClearToChatGptEntries();

        Assert.IsEmpty(GameLogger.ToChatGptEntries);
    }
}
