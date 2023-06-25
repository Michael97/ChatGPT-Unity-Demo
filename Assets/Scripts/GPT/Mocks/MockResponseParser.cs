using System;
using System.Collections;
using System.Collections.Generic;

public class MockResponseParser : ResponseParser
{
    public bool ParseCalled { get; private set; }
    public bool ExecuteCalled { get; private set; }
    public string ResponseToParse { get; private set; }

    public MockResponseParser(MockActionFactory mockActionFactory)
        : base()
    {
        actionFactory = mockActionFactory;
    }

    public override List<IAction> Parse(string response)
    {
        ParseCalled = true;
        ResponseToParse = response;

        return base.Parse(response);
    }

    public override IEnumerator Execute(List<IAction> actions, Action callback, Dictionary<IAction, string> actionResults)
    {
        ExecuteCalled = true;
        yield return base.Execute(actions, callback, actionResults);
    }
}
