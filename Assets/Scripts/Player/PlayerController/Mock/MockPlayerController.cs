using System;
using System.Collections;
using UnityEngine;

public class MockPlayerController : PlayerController
{
    public override void Move(Vector2 direction, Action onFinish)
    {
        if (IsPathBlocked(direction)) return;

        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + direction;

        // Clamp final position to ensure it's aligned to the grid
        endPosition.x = Mathf.Round(endPosition.x);
        endPosition.y = Mathf.Round(endPosition.y);
        transform.position = endPosition;
    }
}
