using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어 동작을 제어하는 스크립트
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("플레이어 스테이터스")]
    [SerializeField] private float moveSpeed;
    private Vector2 curMoveInput;

    private Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 보이지 않게 고정

        SetPlayerStatus();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Move();
    }

    private void SetPlayerStatus()
    {
        moveSpeed = 5f;
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;

        dir *= moveSpeed;
        dir.y = _rb.velocity.y; // y축 속도는 유지

        _rb.velocity = dir;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMoveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMoveInput = Vector2.zero;
        }
    }
}
