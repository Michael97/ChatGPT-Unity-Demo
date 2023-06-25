using System.Collections.Generic;

public interface IInventory
{
    bool AddItem(IItem item);
    bool RemoveItem(IItem item);
    bool RemoveItem(string itemName, int quantity);
    IItem GetItem(string itemName);
    List<IItem> GetInventory();
    void UseItem(IItem item, Player player);
}
