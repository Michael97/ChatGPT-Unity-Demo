using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController
{
    private Rigidbody2D m_rigidbody;
    private BoxCollider2D m_collider;

    public float Speed => m_speed;
    [SerializeField] private float m_speed = 1.0f;

    public bool IsMoving => m_isMoving;
    private bool m_isMoving = false;

    public bool IsPlayer => m_isPlayer;
    [SerializeField] private bool m_isPlayer = true;

    public Vector2 FacingDirection => m_facingDirection;
    private Vector2 m_facingDirection;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<BoxCollider2D>();
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
        return m_collider.bounds.center;
    }


    public virtual void Move(Vector2 direction, Action onFinish)
    {
        if (IsPathBlocked(direction)) return;

        StartCoroutine(MoveCoroutine(direction, onFinish));
    }

    public virtual IEnumerator MoveCoroutine(Vector2 direction, Action onFinish)
    {
        m_isMoving = true;
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + direction;

        float elapsedTime = 0;
        float moveDuration = 1.0f / Speed;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            m_rigidbody.MovePosition(Vector2.Lerp(startPosition, endPosition, elapsedTime / moveDuration));
            yield return null;
        }

        // Clamp final position to ensure it's aligned to the grid
        endPosition.x = Mathf.Round(endPosition.x);
        endPosition.y = Mathf.Round(endPosition.y);
        m_rigidbody.MovePosition(endPosition);

        m_isMoving = false;

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
            m_isMoving = true;
            Move(directionVector, () =>
            {
                GameLogger.LogMessage($"Moving in {directionVector}, current pos {(Vector2)gameObject.transform.position}", LogType.Low);
                onMoveFinished?.Invoke();
                m_isMoving = false;
            });

            // Wait for the move to finish
            yield return new WaitUntil(() => !m_isMoving);

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

        Vector2 startPosition = m_collider.bounds.center;
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