using System.Collections;
using System.IO;
using NUnit.Framework;
using UnityEngine;

public class GameLoggerUpdaterTests
{
    private GameObject gameLoggerUpdaterGameObject;
    private GameLoggerUpdater gameLoggerUpdater;

    [SetUp]
    public void Setup()
    {
        gameLoggerUpdaterGameObject = new GameObject();
        gameLoggerUpdater = gameLoggerUpdaterGameObject.AddComponent<GameLoggerUpdater>();
        gameLoggerUpdater.LogFileName = "test_game_logs.txt";

        if (File.Exists(gameLoggerUpdater.LogFileName))
        {
            File.Delete(gameLoggerUpdater.LogFileName);
        }
    }

    [TearDown]
    public void Teardown()
    {
        if (gameLoggerUpdaterGameObject != null)
        {
            GameObject.Destroy(gameLoggerUpdaterGameObject);
        }

        if (File.Exists(gameLoggerUpdater.LogFileName))
        {
            File.Delete(gameLoggerUpdater.LogFileName);
        }
    }

    [Test]
    public void LogFileIsUpdatedEveryTenthOfASecond()
    {
        RunCoroutine(LogFileIsUpdatedEveryTenthOfASecondCoroutine()).MoveNext();
    }

    private IEnumerator LogFileIsUpdatedEveryTenthOfASecondCoroutine()
    {
        float testDuration = 0.5f; // Test for 0.5 seconds
        float elapsedTime = 0f;

        while (elapsedTime < testDuration)
        {
            elapsedTime += Time.deltaTime;
            GameLogger.LogMessage("Test message", LogType.Low);
            yield return null;
        }

        // Wait for two update intervals to ensure log updates have occurred
        yield return new WaitForSeconds(gameLoggerUpdater.UpdateInterval * 2);

        Assert.IsTrue(File.Exists(gameLoggerUpdater.LogFileName));
        string logContent = File.ReadAllText(gameLoggerUpdater.LogFileName);
        Assert.IsTrue(logContent.Contains("Test message"));
    }

    private IEnumerator RunCoroutine(IEnumerator coroutine)
    {
        while (coroutine.MoveNext())
        {
            yield return coroutine.Current;
        }
    }
}
