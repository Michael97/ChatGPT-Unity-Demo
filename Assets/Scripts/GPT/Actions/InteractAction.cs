using System;
using System.Collections;

public class InteractAction : IAction
{
    private Player player;
    public string[] Parameters { get; set; }
    public bool ExecuteCalled { get; set; }
    public bool CancelCalled { get; set; }

    public InteractAction(Player player)
    {
        this.player = player;
    }


    public IEnumerator Execute(string[] parameters, Action<string> onFinish)
    {
        ExecuteCalled = true;
        player.Interaction.OnInteractPressed(player);

        // Since there is no asynchronous operation in this action, we can call onFinish immediately.
        onFinish?.Invoke("Interacted");
        yield return null;
    }

    public void Cancel()
    {
        // Nothing to cancel for GetVisionAction
    }
}
