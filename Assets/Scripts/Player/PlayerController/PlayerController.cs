using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController
{
    public Rigidbody2D Rigidbody { get; private set; }
    public BoxCollider2D Collider { get; private set; }
    public float Speed { get; private set; } = 1.0f;
    public bool IsMoving { get; private set; } = false;
    public bool IsPlayer { get; private set; } = true;
    public Vector2 FacingDirection { get; private set; }


    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();
    }

    public Vector2 GetPlayerFacingDirection()
    {
        return FacingDirection;
    }
    public Vector2 GetPlayerPosition()
    {
        return (Vector2)transform.position;
    }

    public Vector2 GetColliderPlayerPosition()
    {
        return Collider.bounds.center;
    }


    public virtual void Move(Vector2 direction, Action onFinish)
    {
        if (IsPathBlocked(direction)) return;

        StartCoroutine(MoveCoroutine(direction, onFinish));
    }

    public virtual IEnumerator MoveCoroutine(Vector2 direction, Action onFinish)
    {
        IsMoving = true;
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + direction;

        float elapsedTime = 0;
        float moveDuration = 1.0f / Speed;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            Rigidbody.MovePosition(Vector2.Lerp(startPosition, endPosition, elapsedTime / moveDuration));
            yield return null;
        }

        // Clamp final position to ensure it's aligned to the grid
        endPosition.x = Mathf.Round(endPosition.x);
        endPosition.y = Mathf.Round(endPosition.y);
        Rigidbody.MovePosition(endPosition);

        IsMoving = false;

        //GameLogger.LogMessage("You are now at: " + endPosition + " (World position)", LogType.ToChatGpt);

        onFinish?.Invoke();
    }

    public void MoveUnits(ChatGptAgent agent, int unitsToMove, Vector2 directionVector, Action onFinish)
    {
        // Replace the for loop with a call to StartCoroutine
        StartCoroutine(MoveRecursively(agent, unitsToMove, directionVector, onFinish));
    }


    private IEnumerator MoveRecursively(ChatGptAgent agent, int remainingMoves, Vector2 directionVector, Action onMoveFinished)
    {
        if (remainingMoves > 0)
        {
            IsMoving = true;
            Move(directionVector, () =>
            {
                GameLogger.LogMessage($"Moving in {directionVector}, current pos {(Vector2)gameObject.transform.position}", LogType.Low);
                onMoveFinished?.Invoke();
                IsMoving = false;
            });

            // Wait for the move to finish
            yield return new WaitUntil(() => !IsMoving);

            if (agent)
            {
                if (agent.DialogueManager)
                {
                    var liveData = agent.LiveDataManager.GetLiveData();
                    GameLogger.LogMessage("Data updated", LogType.Low);
                    agent.DialogueManager.UpdateLiveData(liveData);
                }
            }

            // Move again
            yield return MoveRecursively(agent, remainingMoves - 1, directionVector, onMoveFinished);
        }
    }

    protected bool IsPathBlocked(Vector2 direction)
    {
        float distance = 1.0f;
        LayerMask combinedLayerMask = LayerMask.GetMask("Interactable") | LayerMask.GetMask("Building");

        Vector2 startPosition = Collider.bounds.center;
        Vector2 endPosition = startPosition + direction;

        RaycastHit2D[] hits = Physics2D.RaycastAll(startPosition, direction, distance, combinedLayerMask);

        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                Vector2 hitPosition = hit.transform.position;
                if (Mathf.Approximately(hitPosition.x, startPosition.x) && Mathf.Approximately(hitPosition.y, startPosition.y))
                {
                    // Ignore the collider at the player's current position
                    continue;
                }

                var tileData = hit.collider.GetComponent<BaseTile>().GetCustomTileData();
                if (tileData != null && tileData.IsWalkable)
                {
                    return false;
                }
                else
                {
                    GameLogger.LogMessage($"You can't move there, {hit.collider.GetComponent<BaseTile>().GetCustomTileData().Description} is blocking movement there", LogType.ToChatGpt);
                    return true;
                }
            }
        }
        //GameLogger.LogMessage("You can't move there", LogType.ToChatGpt);
        return false;
    }
}