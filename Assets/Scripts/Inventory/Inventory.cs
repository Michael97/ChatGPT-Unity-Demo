using System.Collections.Generic;
using UnityEngine;

public class Inventory : IInventory
{
    [SerializeField]
    private List<Item> m_items = new List<Item>();

    public bool AddItem(IItem item)
    {
        IItem existingItem = GetItem(item.Name);
        if (existingItem != null)
        {
            existingItem.Quantity += item.Quantity;
        }
        else
        {
            m_items.Add(item as Item);
        }
        return true;
    }

    public bool RemoveItem(IItem item)
    {
        return RemoveItem(item.Name, item.Quantity);
    }

    public bool RemoveItem(string itemName, int quantity)
    {
        IItem existingItem = GetItem(itemName);
        if (existingItem != null && existingItem.Quantity >= quantity)
        {
            existingItem.Quantity -= quantity;
            if (existingItem.Quantity == 0)
            {
                m_items.Remove(existingItem as Item);
            }
            return true;
        }
        return false;
    }

    public IItem GetItem(string itemName)
    {
        return m_items.Find(item => item.Name == itemName);
    }

    public List<IItem> GetInventory()
    {
        GameLogger.LogMessage("Getting inventory...", LogType.Low);
        return new List<IItem>(m_items);
    }

    public void UseItem(IItem item, Player player)
    {
        if (item is IUsableItem usableItem)
        {
            usableItem.Use(player);
        }
        else
        {
            Debug.LogWarning("Item is not usable.");
        }
    }

}
