using System;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private PlayerController m_playerController;

    private void Awake()
    {
        m_playerController = GetComponent<PlayerController>();
        if (m_playerController == null)
        {
            Debug.LogError("PlayerController component not found on the GameObject");
        }
    }

    void Update()
    {
        if (!m_playerController.IsMoving)
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
                m_playerController.Move(moveDirection, () => { });
            }
        }
    }
}
