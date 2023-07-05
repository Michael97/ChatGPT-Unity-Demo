using System;
using System.Collections;

public class InteractAction : IAction
{
    private Player m_player;
    public string[] Parameters { get; set; }
    public bool ExecuteCalled { get; set; }
    public bool CancelCalled { get; set; }

    public InteractAction(Player player)
    {
        this.m_player = player;
    }


    public IEnumerator Execute(string[] parameters, Action<string> onFinish)
    {
        ExecuteCalled = true;
        m_player.Interaction.OnInteractPressed(m_player);

        // Since there is no asynchronous operation in this action, we can call onFinish immediately.
        onFinish?.Invoke("Interacted");
        yield return null;
    }

    public void Cancel()
    {
        
    }
}
