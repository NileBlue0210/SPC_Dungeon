using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� ������ �����ϴ� ��ũ��Ʈ
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("�÷��̾� �������ͽ�")]
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
