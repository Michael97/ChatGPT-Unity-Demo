using System;
using System.Collections;

public class ActionExecutor
{
    public IEnumerator Execute(IAction action, Action<string> callback)
    {
        yield return action.Execute(action.Parameters, result => {
            GameLogger.LogMessage(result, LogType.Low);
            callback(result);
        });
    }
}