using System;
using System.Collections;
using System.Collections.Generic;

public class GetInventoryAction : IAction
{
    public string[] Parameters { get; set; }
    public bool ExecuteCalled { get; set; }
    public bool CancelCalled { get; set; }

    private IInventory m_Inventory;

    public GetInventoryAction(IInventory inventory)
    {
        m_Inventory = inventory;
    }

    public IEnumerator Execute(string[] parameters, Action<string> onFinish)
    {
        List<IItem> items = m_Inventory.GetInventory();
        string inventoryData = "" + string.Join(", ", items);

        onFinish?.Invoke(inventoryData);

        //return inventoryData;
        yield return null;
    }

    public void Cancel()
    {

    }
}
