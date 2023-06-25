public class LootTile : InteractableTile
{
    public override void Interact(Player player)
    {
        GameLogger.LogMessage("You picked up a 'ChickenLeg'", LogType.ToChatGpt);
        player.Inventory.AddItem(new FoodItem(tileDataSO.tileName, 1));
        base.Interact(player);
        Destroy(this.gameObject);
        // Implement berry bush-specific interaction logic here
    }
}