using System;
using UnityEngine;

public class MockGoToAction : GoToAction
{
    public bool MoveInDirectionCalled { get; private set; }
    public string DirectionPassed { get; private set; }
    public int UnitsToMovePassed { get; private set; }

    public MockGoToAction(ChatGptAgent agent) : base(agent)
    {
    }

    protected override void MoveInDirection(ChatGptAgent agent, string direction, int unitsToMove, Action onFinish)
    {
        MoveInDirectionCalled = true;
        DirectionPassed = direction;
        UnitsToMovePassed = unitsToMove;
    }
}
