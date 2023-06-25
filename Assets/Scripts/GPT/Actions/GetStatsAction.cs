using System.Collections;
using UnityEngine;
using System;

public class GetStatsAction : IAction
{
    public string[] Parameters { get; set; }
    public bool ExecuteCalled { get; set; }
    public bool CancelCalled { get; set; }

    private IPlayerStats playerStats;

    public GetStatsAction(IPlayerStats playerStats)
    {
        this.playerStats = playerStats;
    }

    public IEnumerator Execute(string[] parameters, Action<string> onFinish)
    {
        GameLogger.LogMessage("Getting stats...", LogType.Low);

        ExecuteCalled = true;

        string stats = playerStats.GetStats();

        onFinish?.Invoke(stats);

        yield return stats;
    }

    public void Cancel()
    {
        // Nothing to cancel for GetStatsAction
    }
}
