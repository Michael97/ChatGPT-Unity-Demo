using System;
using System.Collections;

public class GetMemoryAction : IAction
{
    public string[] Parameters { get; set; }
    public bool ExecuteCalled { get; set; }
    public bool CancelCalled { get; set; }

    private ChatGptAgent _chatGptAgent;

    public GetMemoryAction(ChatGptAgent chatGptAgent)
    {
        _chatGptAgent = chatGptAgent;
    }

    public IEnumerator Execute(string[] parameters, Action<string> onFinish)
    {
        string memoryKey = parameters[0];
        string memoryValue = $"Memory {parameters[0]}: {_chatGptAgent.memory.GetMemory(memoryKey)}";

        onFinish?.Invoke(memoryValue);
        yield return null;
    }

    public void Cancel()
    {

    }
}
