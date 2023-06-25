using System;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController component not found on the GameObject");
        }
    }

    void Update()
    {
        if (!playerController.IsMoving)
        {
            int horizontal = (int)Input.GetAxisRaw("Horizontal");
            int vertical = (int)Input.GetAxisRaw("Vertical");

            if (horizontal != 0)
            {
                vertical = 0;
            }

            if (horizontal != 0 || vertical != 0)
            {
                Vector2 moveDirection = new Vector2(horizontal, vertical);
                playerController.Move(moveDirection, () => { });
            }
        }
    }
}
