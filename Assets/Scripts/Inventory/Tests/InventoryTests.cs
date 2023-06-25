using NUnit.Framework;
using UnityEngine;

public class InventoryTests
{
    [Test]
    public void TestAddItem()
    {
        var inventory = new MockInventory();
        inventory.AddItem(new Item("Wood", 5));

        IItem item = inventory.GetItem("Wood");

        Assert.IsNotNull(item);
        Assert.AreEqual("Wood", item.Name);
        Assert.AreEqual(5, item.Quantity);
    }

    [Test]
    public void TestRemoveItem()
    {
        var inventory = new MockInventory();
        inventory.AddItem(new Item("Wood", 5));
        inventory.RemoveItem("Wood", 3);

        IItem item = inventory.GetItem("Wood");

        Assert.IsNotNull(item);
        Assert.AreEqual("Wood", item.Name);
        Assert.AreEqual(2, item.Quantity);
    }

    [Test]
    public void TestGetInventory()
    {
        var inventory = new MockInventory();
        inventory.AddItem(new Item("Wood", 5));
        inventory.AddItem(new Item("Stone", 3));

        var items = inventory.GetInventory();

        Assert.AreEqual(2, items.Count);

        IItem woodItem = inventory.GetItem("Wood");
        IItem stoneItem = inventory.GetItem("Stone");

        Assert.IsNotNull(woodItem);
        Assert.AreEqual("Wood", woodItem.Name);
        Assert.AreEqual(5, woodItem.Quantity);

        Assert.IsNotNull(stoneItem);
        Assert.AreEqual("Stone", stoneItem.Name);
        Assert.AreEqual(3, stoneItem.Quantity);
    }
}
