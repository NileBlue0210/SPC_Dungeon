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

    [Header("플레이어 시야")]
    public Transform cameraContainer; // Camera 컴포넌트를 담고있는 Player 자식 오브젝트
    public float minXLook;
    public float maxXLook;
    public float lookSensitivity;  // 마우스 민감도
    private float camCurXRotation;  // 상하 시야 각 (카메라를 통해 제어)
    private float charCurYRotation;  // 좌우 시야 각 (캐릭터를 직접 회전)
    private Vector2 mouseDelta;

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
        camCurXRotation = Mathf.Clamp(-camCurXRotation, minXLook, maxXLook); // 쿼터니언값이므로 오일러 각도로 변환할 필요가 있다
        cameraContainer.localEulerAngles = new Vector3(0, mouseDelta.x * lookSensitivity, 0);   // 카메라 y축 각도 조정. 카메라의 Player기준의 로컬 좌표값을 변경하기 때문에 좌우 회전을 위해 x값을 변경

        charCurYRotation += mouseDelta.x * lookSensitivity;
        transform.eulerAngles = new Vector3(0, charCurYRotation, 0); // 캐릭터 y축 각도 조정. 월드 좌표 기준이기 때문에 좌우 회전을 위해 y값을 변경
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
}
