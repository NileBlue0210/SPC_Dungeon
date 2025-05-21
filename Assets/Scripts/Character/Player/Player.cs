using UnityEngine;


/// <summary>
/// �÷��̾� ������Ʈ�� �ñ״�ó Ŭ����
/// </summary>
public class Player : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerCondition _playerCondition;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerCondition = GetComponent<PlayerCondition>();
    }

    private void Start()
    {
        UnitManager.Instance.Player = this;
    }
}
