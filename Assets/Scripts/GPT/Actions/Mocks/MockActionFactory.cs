using System.Collections.Generic;

public class MockActionFactory : IActionFactory
{
    private Dictionary<string, IAction> actionMap;
    private Dictionary<string, System.Func<IAction>> actionCreators;

    public MockActionFactory(MockPlayerStats mockPlayerStats, ChatGptAgent mockAgent)
    {
        actionMap = new Dictionary<string, IAction>
        {
            { "getStats", new MockGetStatsAction(mockPlayerStats) },
            { "goTo", new MockGoToAction(mockAgent) },
            { "getVision", new MockGetVisionAction(mockAgent, "Mock vision data") }
        };
    }

    public IAction GetAction(string actionName)
    {
        IAction action;

        if (actionMap.TryGetValue(actionName, out action))
        {
            return action;
        }

        return null;
    }

    public void RegisterAction(string actionName, IAction action)
    {
        actionCreators[actionName] = () => action;
    }
}
