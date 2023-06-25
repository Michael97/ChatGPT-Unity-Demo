using System.Collections;
using System;

public interface IAction
{
    string[] Parameters { get; set; }
    bool ExecuteCalled { get; set; }
    bool CancelCalled { get; set; }
    IEnumerator Execute(string[] parameters, Action<string> onFinish);
    void Cancel();
}