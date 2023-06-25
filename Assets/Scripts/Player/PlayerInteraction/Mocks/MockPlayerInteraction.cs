public class MockPlayerInteraction : PlayerInteraction
{
    public bool OnInteractPressedCalled { get; private set; }

    public override void OnInteractPressed(Player player)
    {
        OnInteractPressedCalled = true;
    }
}
