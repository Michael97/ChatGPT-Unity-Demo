using System.Collections.Generic;

public class ActionFactory : IActionFactory
{
    private Dictionary<string, IAction> actionMap;
    private Dictionary<string, System.Func<IAction>> actionCreators;

    public ActionFactory(ChatGptAgent agent)
    {
        actionMap = new Dictionary<string, IAction>
        {
            { "move", new GoToAction(agent) },
            { "interact", new InteractAction(agent.Player) },
            { "pickupItem", new InteractAction(agent.Player) },
            { "setMemory", new SetMemoryAction(agent) },
            { "getMemory", new GetMemoryAction(agent) },
            { "useItem", new UseItemAction(agent.Player) },
            
            //Data functions that should not be called by the agent itself.
            { "adminGetPlayerPosition", new GetPlayerPositionAction(agent.Player.Controller) },
            { "adminGetStats", new GetStatsAction(agent.Player.Stats) },
            { "adminGetVision", new GetVisionAction(agent) },
            { "adminGetInventory", new GetInventoryAction(agent.Player.Inventory) },
            { "adminGetMemoryKeys", new GetAllMemoryAction(agent) }

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
