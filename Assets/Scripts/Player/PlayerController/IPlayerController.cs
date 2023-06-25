using System;
using System.Collections;
using UnityEngine;

public interface IPlayerController
{
    Vector2 GetPlayerFacingDirection();
    Vector2 GetPlayerPosition();
    void Move(Vector2 direction, Action onFinish);
}