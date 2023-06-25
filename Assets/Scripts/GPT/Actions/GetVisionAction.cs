using System;
using System.Collections;

public class GetVisionAction : IAction
{
    public string[] Parameters { get; set; }
    public bool ExecuteCalled { get; set; }
    public bool CancelCalled { get; set; }

    private ChatGptAgent agent;

    public GetVisionAction(ChatGptAgent agent)
    {
        this.agent = agent;
    }

    public IEnumerator Execute(string[] parameters, Action<string> onFinish)
    {
        GameLogger.LogMessage("Getting vision...", LogType.Low);

        ExecuteCalled = true; 

        string visionData = agent.ChatGptAgentSensory.GetVision(agent.Player.Controller.GetColliderPlayerPosition());

        onFinish?.Invoke(visionData);

        //GameLogger.LogMessage("GetVision() successfully ran", LogType.FunctionExecution);

        yield return visionData;
    }

    public void Cancel()
    {
        // Nothing to cancel for GetVisionAction
    }
}
