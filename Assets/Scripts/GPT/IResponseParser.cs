using System;
using System.Collections;
using System.Collections.Generic;

public interface IResponseParser
{
    List<IAction> Parse(string response);
    IEnumerator Execute(List<IAction> actions, Action callback, Dictionary<IAction, string> actionResults);
}
