using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float m_interactionRange = 1.0f;
    public LayerMask m_interactionLayers;

    void Update()
    {
        //DebugDrawCircle();
    }

    public virtual void OnInteractPressed(Player player)
    {
        if (!player.Controller.IsMoving || !player.Controller.IsPlayer)
        {
            var interactableTile = GetClosestInteractable(player);

            if (interactableTile != null)
            {
                interactableTile.Interact(player);
            }
            else
            {
                GameLogger.LogMessage("You must be within one tile to interact", LogType.ToChatGpt);
            }
        }
        else
        {
            GameLogger.LogMessage("You must be within one tile to interact", LogType.ToChatGpt);
        }
    }

    void DebugDrawCircle()
    {
        Vector2 center = transform.GetComponent<Collider2D>().bounds.center;
        float radius = m_interactionRange;
        int numSegments = 360;
        float angle = 0;

        for (int i = 0; i < numSegments; i++)
        {
            Vector2 startPosition = center + new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle) * radius, Mathf.Sin(Mathf.Deg2Rad * angle) * radius);
            angle += 360f / numSegments;
            Vector2 endPosition = center + new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle) * radius, Mathf.Sin(Mathf.Deg2Rad * angle) * radius);
            Debug.DrawLine(startPosition, endPosition, Color.red);
        }
    }

    private IInteractableTile GetClosestInteractable(Player player)
    {
        var center = player.Controller.GetColliderPlayerPosition();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, m_interactionRange, m_interactionLayers);
        Vector2 playerFacingDirection = player.Controller.GetPlayerFacingDirection();

        IInteractableTile closestInFacingDirection = null;
        IInteractableTile closestInOtherDirections = null;
        IInteractableTile onTopInteractable = null;

        float minDistanceOtherDirections = float.MaxValue;

        foreach (Collider2D collider in colliders)
        {
            Vector2 directionToObject = ((Vector2)collider.bounds.center - center).normalized;
            float dotProduct = Vector2.Dot(playerFacingDirection, directionToObject);
            float closeDist = Vector2.Distance(center, collider.bounds.center);
            // Check if the player is directly on top of the interactable
            if (Vector2.Distance(center, collider.bounds.center) < 0.1f)
            {
                var interactable = collider.GetComponent<IInteractableTile>();

                if (interactable == null)
                    continue;

                onTopInteractable = interactable;
                break;
            }
            else if (Mathf.Approximately(dotProduct, 1)) // Check if the dot product is 1 (the object is in the facing direction)
            {
                var interactable = collider.GetComponent<IInteractableTile>();

                if (interactable == null)
                    continue;

                closestInFacingDirection = interactable;
                continue;
            }
            else if (Mathf.Approximately(dotProduct, 0) || Mathf.Approximately(dotProduct, -1)) // Check if the dot product is 0 (North, West, South, or East) or - 1
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < minDistanceOtherDirections)
                {
                    var interactable = collider.GetComponent<IInteractableTile>();

                    if (interactable != null)
                    {
                        minDistanceOtherDirections = distance;
                        closestInOtherDirections = interactable;
                    }
                }
            }
        }

        if (onTopInteractable != null)
        {
            return onTopInteractable;
        }

        return closestInFacingDirection != null ? closestInFacingDirection : closestInOtherDirections;
    }

}
