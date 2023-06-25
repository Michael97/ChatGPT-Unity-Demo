using System.Collections;
using UnityEngine;
using System;

public class GetPlayerPositionAction : IAction
{
    public string[] Parameters { get; set; }
    public bool ExecuteCalled { get; set; }
    public bool CancelCalled { get; set; }

    private IPlayerController playerController;

    public GetPlayerPositionAction(IPlayerController playerController)
    {
        this.playerController = playerController;
    }

    public IEnumerator Execute(string[] parameters, Action<string> onFinish)
    {
        GameLogger.LogMessage("Getting player location...", LogType.Low);

        ExecuteCalled = true;

        var position = playerController.GetPlayerPosition();

        var positionString = $"({(int)position.x},{(int)position.y})";

        onFinish?.Invoke(positionString);

        yield return positionString;
    }

    public void Cancel()
    {
        // Nothing to cancel for GetStatsAction
    }
}
