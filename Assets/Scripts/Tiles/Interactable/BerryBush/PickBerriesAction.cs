public class PickBerriesAction : IInteractionAction
{
    public void Execute(InteractableTile tile)
    {
        // Implement fruit picking logic here
        GameLogger.LogMessage($"You picked up berries", LogType.ToChatGpt);
    }
}