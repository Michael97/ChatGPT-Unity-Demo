using System;
using System.Collections;

public class UseItemAction : IAction
{
    private Player m_player;
    public string[] Parameters { get; set; }
    public bool ExecuteCalled { get; set; }
    public bool CancelCalled { get; set; }

    public UseItemAction(Player player)
    {
        this.m_player = player;
    }

    public IEnumerator Execute(string[] parameters, Action<string> onFinish)
    {
        ExecuteCalled = true;

        string itemName = parameters[0].Trim('\'', ' ');
        IItem item = m_player.Inventory.GetItem(itemName);

        if (item != null)
        {
            m_player.Inventory.UseItem(item, m_player);
            onFinish?.Invoke($"Used Item: {itemName}");
        }
        else
        {
            onFinish?.Invoke($"Item not found: {itemName}");
        }

        yield return null;
    }

    public void Cancel()
    {

    }
}
