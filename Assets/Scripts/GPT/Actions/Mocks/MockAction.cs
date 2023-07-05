using System;
using System.Collections;
using UnityEngine;

public class MockAction : IAction
{
    public string[] Parameters { get; set; }
    public bool ExecuteCalled { get; set; }
    public bool CancelCalled { get; set; }

    public void Execute(string[] parameters, Action onFinish)
    {
        ExecuteCalled = true;

        Debug.Log("MockAction Executed");
        // Implement your logic to get stats here

        onFinish?.Invoke();
    }


    public void Cancel()
    {
        CancelCalled = true;
    }

    public IEnumerator Execute(string[] parameters, Action<string> onFinish)
    {
        ExecuteCalled = true;

        // Since there is no asynchronous operation in this action, we can call onFinish immediately.
        onFinish?.Invoke("Interacted");
        yield return null;
    }
}
