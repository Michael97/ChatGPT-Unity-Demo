using System.Collections;
using UnityEngine;
using System;

public class GetStatsAction : IAction
{
    public string[] Parameters { get; set; }
    public bool ExecuteCalled { get; set; }
    public bool CancelCalled { get; set; }

    private IPlayerStats iPlayerStats;

    public GetStatsAction(IPlayerStats iPlayerStats)
    {
        this.iPlayerStats = iPlayerStats;
    }

    public IEnumerator Execute(string[] parameters, Action<string> onFinish)
    {
        GameLogger.LogMessage("Getting stats...", LogType.Low);

        ExecuteCalled = true;

        string stats = iPlayerStats.GetStats();

        onFinish?.Invoke(stats);

        yield return stats;
    }

    public void Cancel()
    {

    }
}
