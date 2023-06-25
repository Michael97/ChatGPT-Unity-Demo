using System.Diagnostics;

[System.Serializable]
public class Item : IUsableItem
{
    public string Name { get; private set; }
    public int Quantity { get; set; }

    public Item(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
    }

    public virtual void Use(Player player)
    {
        GameLogger.LogMessage($"{Name} used from inventory", LogType.ToChatGpt);
    }
}
