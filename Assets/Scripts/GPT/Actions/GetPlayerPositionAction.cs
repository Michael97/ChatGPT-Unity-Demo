using System.Collections;
using UnityEngine;
using System;

public class GetPlayerPositionAction : IAction
{
    public string[] Parameters { get; set; }
    public bool ExecuteCalled { get; set; }
    public bool CancelCalled { get; set; }

    private IPlayerController m_iPlayerController;

    public GetPlayerPositionAction(IPlayerController iPlayerController)
    {
        this.m_iPlayerController = iPlayerController;
    }

    public IEnumerator Execute(string[] parameters, Action<string> onFinish)
    {
        GameLogger.LogMessage("Getting player location...", LogType.Low);

        ExecuteCalled = true;

        var position = m_iPlayerController.GetPlayerPosition();

        var positionString = $"({(int)position.x},{(int)position.y})";

        onFinish?.Invoke(positionString);

        yield return positionString;
    }

    public void Cancel()
    {

    }
}
