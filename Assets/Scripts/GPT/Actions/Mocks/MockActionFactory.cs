using System.Collections.Generic;

public class MockActionFactory : IActionFactory
{
    private Dictionary<string, IAction> m_actionMap;
    private Dictionary<string, System.Func<IAction>> m_actionCreators;

    public MockActionFactory(MockPlayerStats mockPlayerStats, ChatGptAgent mockAgent)
    {
        m_actionMap = new Dictionary<string, IAction>
        {
            { "getStats", new MockGetStatsAction(mockPlayerStats) },
            { "goTo", new MockGoToAction(mockAgent) },
            { "getVision", new MockGetVisionAction(mockAgent, "Mock vision data") }
        };
    }

    public IAction GetAction(string actionName)
    {
        IAction action;

        if (m_actionMap.TryGetValue(actionName, out action))
        {
            return action;
        }

        return null;
    }

    public void RegisterAction(string actionName, IAction action)
    {
        m_actionCreators[actionName] = () => action;
    }
}
