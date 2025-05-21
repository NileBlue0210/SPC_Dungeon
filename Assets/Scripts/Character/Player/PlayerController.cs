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

    [Header("�÷��̾� �þ�")]
    public Transform cameraContainer; // Camera ������Ʈ�� ����ִ� Player �ڽ� ������Ʈ
    public float minXLook;
    public float maxXLook;
    public float lookSensitivity;  // ���콺 �ΰ���
    private float camCurXRotation;  // ���� �þ� �� (ī�޶� ���� ����)
    private float charCurYRotation;  // �¿� �þ� �� (ĳ���͸� ���� ȸ��)
    private Vector2 mouseDelta;

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

    private void LateUpdate()
    {
        CameraLook();
    }

    private void SetPlayerStatus()
    {
        moveSpeed = 5f;
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
        camCurXRotation = Mathf.Clamp(-camCurXRotation, minXLook, maxXLook); // ���ʹϾ��̹Ƿ� ���Ϸ� ������ ��ȯ�� �ʿ䰡 �ִ�
        cameraContainer.localEulerAngles = new Vector3(0, mouseDelta.x * lookSensitivity, 0);   // ī�޶� y�� ���� ����. ī�޶��� Player������ ���� ��ǥ���� �����ϱ� ������ �¿� ȸ���� ���� x���� ����

        charCurYRotation += mouseDelta.x * lookSensitivity;
        transform.eulerAngles = new Vector3(0, charCurYRotation, 0); // ĳ���� y�� ���� ����. ���� ��ǥ �����̱� ������ �¿� ȸ���� ���� y���� ����
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
}
