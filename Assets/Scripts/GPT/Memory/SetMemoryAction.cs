using System;
using System.Collections;

public class SetMemoryAction : IAction
{
    public string[] Parameters { get; set; }
    public bool ExecuteCalled { get; set; }
    public bool CancelCalled { get; set; }

    private ChatGptAgent _chatGptAgent;

    public SetMemoryAction(ChatGptAgent chatGptAgent)
    {
        _chatGptAgent = chatGptAgent;
    }

    public IEnumerator Execute(string[] parameters, Action<string> onFinish)
    {
        string memoryKey = parameters[0];
        string memoryValue = parameters[1];
        _chatGptAgent.memory.AddMemory(memoryKey, memoryValue);

        string response = $"Memory {parameters[0]} saved";

        onFinish?.Invoke(response);
        yield return null;
    }

    public void Cancel()
    {
        // Nothing to cancel for SetMemoryAction
    }
}
