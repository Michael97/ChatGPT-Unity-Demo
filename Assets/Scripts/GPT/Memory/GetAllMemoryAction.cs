using System;
using System.Collections;
using System.Linq;

public class GetAllMemoryAction : IAction
{
    public string[] Parameters { get; set; }
    public bool ExecuteCalled { get; set; }
    public bool CancelCalled { get; set; }

    private ChatGptAgent _chatGptAgent;

    public GetAllMemoryAction(ChatGptAgent chatGptAgent)
    {
        _chatGptAgent = chatGptAgent;
    }

    public IEnumerator Execute(string[] parameters, Action<string> onFinish)
    {
        var memoryNames = _chatGptAgent.memory.GetAllMemoryNames();
        string memoryNamesString = "Memory Keys: " + string.Join(", ", memoryNames);

        onFinish?.Invoke(memoryNamesString);
        yield return null;
    }

    public void Cancel()
    {
        // Nothing to cancel for GetAllMemoryAction
    }
}
