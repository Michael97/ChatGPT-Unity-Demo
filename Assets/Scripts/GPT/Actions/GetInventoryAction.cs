using System;
using System.Collections;
using System.Collections.Generic;

public class GetInventoryAction : IAction
{
    public string[] Parameters { get; set; }
    public bool ExecuteCalled { get; set; }
    public bool CancelCalled { get; set; }

    private IInventory _inventory;

    public GetInventoryAction(IInventory inventory)
    {
        _inventory = inventory;
    }

    public IEnumerator Execute(string[] parameters, Action<string> onFinish)
    {
        List<IItem> items = _inventory.GetInventory();
        string inventoryData = "" + string.Join(", ", items);

        onFinish?.Invoke(inventoryData);

        //return inventoryData;
        yield return null;
    }

    public void Cancel()
    {
        // Nothing to cancel for GetInventoryAction
    }
}
