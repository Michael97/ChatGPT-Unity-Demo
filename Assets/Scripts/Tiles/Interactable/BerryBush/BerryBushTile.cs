using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBushTile : InteractableTile
{
    [SerializeField]
    private List<IInteractionAction> interactionActions;

    [SerializeField]
    private int maxBerries = 20;

    [SerializeField]
    private float berryGrowthInterval = 10f; // Time interval in seconds between berry growth

    public int AvailableBerries { get; private set; } = 10;

    private void Awake()
    {
        interactionActions = new List<IInteractionAction>
        {
            new PickBerriesAction()
        };

        // Start the berry growth coroutine
        StartCoroutine(GrowBerries());
    }

    public override void Interact(Player player)
    {
        base.Interact(player);

        IInteractionAction selectedAction = GetDesiredInteractionAction();

        if (selectedAction != null)
        {
            selectedAction.Execute(this);
        }
    }

    private IInteractionAction GetDesiredInteractionAction()
    {
        if (AvailableBerries > 0)
        {
            IInteractionAction pickBerriesAction = interactionActions.Find(action => action.GetType() == typeof(PickBerriesAction));
            return pickBerriesAction;
        }
        else
        {
            return null;
        }
    }

    // Coroutine to grow/replenish berries over time
    private IEnumerator GrowBerries()
    {
        while (true)
        {
            // Wait for the specified time interval
            yield return new WaitForSeconds(berryGrowthInterval);

            // Increase the available berries count, up to the maximum
            AvailableBerries = Mathf.Min(AvailableBerries + 1, maxBerries);
        }
    }
}
