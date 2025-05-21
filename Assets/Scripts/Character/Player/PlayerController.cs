using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 동작을 제어하는 스크립트
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("플레이어 스테이터스")]
    [SerializeField] private float moveSpeed;
    private Vector2 curMoveInput;

    // Start is called before the first frame update
    void Start()
    {
        SetPlayerStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    private void SetPlayerStatus()
    {
        moveSpeed = 5f;
    }

    private void Move()
    {
        
    }
}
