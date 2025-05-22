using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어 동작을 제어하는 스크립트
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private Vector2 curMoveInput;

    [Header("Jump")]
    [SerializeField] private float jumpForce;


    [Header("Sight")]
    public Transform cameraContainer; // Camera 컴포넌트를 담고있는 Player 자식 오브젝트
    public float minXLook;
    public float maxXLook;
    public float lookSensitivity;  // 마우스 민감도
    private float camCurXRotation;  // 상하 시야 각 (카메라를 통해 제어)
    private float charCurYRotation;  // 좌우 시야 각 (캐릭터를 직접 회전)
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
        Vector3 dir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;    // 4방향으로 이동하기 위해 forward와 right방향의 벡터를 합성

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

    // 사람으로 가정하면, 고개를 좌우로 돌릴 때에 캐릭터를 좌우로 회전시켜야 하고, 시야를 위아래로 올릴 때에는 카메라(고개)의 각도를 조정해야 한다.
    void CameraLook()
    {
        camCurXRotation += mouseDelta.y * lookSensitivity;
        camCurXRotation = Mathf.Clamp(camCurXRotation, minXLook, maxXLook); // 쿼터니언값이므로 오일러 각도로 변환할 필요가 있다
        cameraContainer.localEulerAngles = new Vector3(-camCurXRotation, 0, 0);   // 카메라 y축 각도 조정. 카메라의 Player기준의 로컬 좌표값을 변경하기 때문에 좌우 회전을 위해 x값을 변경

        charCurYRotation += mouseDelta.x * lookSensitivity;
        transform.eulerAngles = new Vector3(0, charCurYRotation, 0); // 캐릭터 y축 각도 조정. 월드 좌표 기준이기 때문에 좌우 회전을 위해 y값을 변경
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
        Ray[] rays = new Ray[4] // 플레이어 주위로 4개의 Ray를 쏘아 땅에 닿아있는지 체크하기 위한 Ray 배열
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),    // 오브젝트 전방 윗부근에 아래로 Ray를 쏘아내는 포인트를 잡는다
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),    // 오브젝트 후방 윗부근에 아래로 Ray를 쏘아내는 포인트를 잡는다
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),    // 오브젝트 우상 부근에 아래로 Ray를 쏘아내는 포인트를 잡는다
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),    // 오브젝트 좌상 부근에 아래로 Ray를 쏘아내는 포인트를 잡는다
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))    // Ground에 닿은 Ray가 하나라도 있다면 땅에 닿은 것으로 간주 ( 2번째 인수인 0.1f는 Ray의 길이 )
            {
                return true;
            }
        }

        return false;
    }
}
