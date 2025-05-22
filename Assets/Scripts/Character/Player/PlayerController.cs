using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �÷��̾� ������ �����ϴ� ��ũ��Ʈ
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private Vector2 curMoveInput;

    [Header("Jump")]
    [SerializeField] private float jumpForce;


    [Header("Sight")]
    public Transform cameraContainer; // Camera ������Ʈ�� ����ִ� Player �ڽ� ������Ʈ
    public float minXLook;
    public float maxXLook;
    public float lookSensitivity;  // ���콺 �ΰ���
    private float camCurXRotation;  // ���� �þ� �� (ī�޶� ���� ����)
    private float charCurYRotation;  // �¿� �þ� �� (ĳ���͸� ���� ȸ��)
    private Vector2 mouseDelta;

    [Header("Interaction")]
    public LayerMask groundLayerMask;

    public Rigidbody _rb;

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

    private void LateUpdate()
    {
        CameraLook();
    }

    private void SetPlayerStatus()
    {
        moveSpeed = 5f;
        jumpForce = 80f;
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;    // 4�������� �̵��ϱ� ���� forward�� right������ ���͸� �ռ�

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

    // ������� �����ϸ�, ���� �¿�� ���� ���� ĳ���͸� �¿�� ȸ�����Ѿ� �ϰ�, �þ߸� ���Ʒ��� �ø� ������ ī�޶�(��)�� ������ �����ؾ� �Ѵ�.
    void CameraLook()
    {
        camCurXRotation += mouseDelta.y * lookSensitivity;
        camCurXRotation = Mathf.Clamp(camCurXRotation, minXLook, maxXLook); // ���ʹϾ��̹Ƿ� ���Ϸ� ������ ��ȯ�� �ʿ䰡 �ִ�
        cameraContainer.localEulerAngles = new Vector3(-camCurXRotation, 0, 0);   // ī�޶� y�� ���� ����. ī�޶��� Player������ ���� ��ǥ���� �����ϱ� ������ �¿� ȸ���� ���� x���� ����

        charCurYRotation += mouseDelta.x * lookSensitivity;
        transform.eulerAngles = new Vector3(0, charCurYRotation, 0); // ĳ���� y�� ���� ����. ���� ��ǥ �����̱� ������ �¿� ȸ���� ���� y���� ����
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4] // �÷��̾� ������ 4���� Ray�� ��� ���� ����ִ��� üũ�ϱ� ���� Ray �迭
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),    // ������Ʈ ���� ���αٿ� �Ʒ��� Ray�� ��Ƴ��� ����Ʈ�� ��´�
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),    // ������Ʈ �Ĺ� ���αٿ� �Ʒ��� Ray�� ��Ƴ��� ����Ʈ�� ��´�
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),    // ������Ʈ ��� �αٿ� �Ʒ��� Ray�� ��Ƴ��� ����Ʈ�� ��´�
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),    // ������Ʈ �»� �αٿ� �Ʒ��� Ray�� ��Ƴ��� ����Ʈ�� ��´�
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))    // Ground�� ���� Ray�� �ϳ��� �ִٸ� ���� ���� ������ ���� ( 2��° �μ��� 0.1f�� Ray�� ���� )
            {
                return true;
            }
        }

        return false;
    }
}
