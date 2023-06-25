using System;
using System.Collections;
using UnityEngine;

public class GoToAction : IAction
{
    private ChatGptAgent agent;
    private bool cancelMovement;
    public string[] Parameters { get; set; }
    public bool ExecuteCalled { get; set; }
    public bool CancelCalled { get; set; }


    public GoToAction(ChatGptAgent agent)
    {
        this.agent = agent;
        this.cancelMovement = false;
    }

    public IEnumerator Execute(string[] parameters, Action<string> onFinish)
    {
        string direction = parameters[0].Trim('\'', ' ');

        int unitsToMove = 1;
        if (parameters.Length > 1)
        {
            int.TryParse(parameters[1], out unitsToMove);
        }

        MoveInDirection(agent, direction, unitsToMove, () => onFinish?.Invoke("Done"));

        string message = $"Successfully moved in direction... {direction} for {unitsToMove} units";

        onFinish?.Invoke("Done");

        //GameLogger.LogMessage(message, LogType.FunctionExecution);

        yield return message; 
    }

    public void Cancel()
    {
        cancelMovement = true;
    }

    protected virtual void MoveInDirection(ChatGptAgent agent, string direction, int unitsToMove, Action onFinish)
    {
        Vector2 directionVector;
        switch (direction.ToLower())
        {
            case "north":
            case "up":
                directionVector = Vector2.up;
                break;
            case "south":
            case "down":
                directionVector = Vector2.down;
                break;
            case "west":
            case "left":
                directionVector = Vector2.left;
                break;
            case "east":
            case "right":
                directionVector = Vector2.right;
                break;
            default:
                GameLogger.LogMessage("Unknown direction entered. Please enter up, down, left, or right for direction", LogType.ToChatGpt);
                return;
        }

        cancelMovement = false;

        agent.Player.Controller.MoveUnits(agent, unitsToMove, directionVector, () => {
            //GameLogger.LogMessage($"GoTo Request Complete: Current world position is {(Vector2)playerController.gameObject.transform.position}", LogType.ToChatGpt);
            onFinish?.Invoke();
        });

    }
}
