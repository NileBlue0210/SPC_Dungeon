using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �÷��̾� ������ �����ϴ� ��ũ��Ʈ
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("�÷��̾� �������ͽ�")]
    [SerializeField] private float moveSpeed;
    private Vector2 curMoveInput;

    private Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ������ �ʰ� ����

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
        dir.y = _rb.velocity.y; // y�� �ӵ��� ����

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
