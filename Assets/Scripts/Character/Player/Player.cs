using UnityEngine;


/// <summary>
/// �÷��̾� ������Ʈ�� �ñ״�ó Ŭ����
/// </summary>
public class Player : MonoBehaviour
{
    public PlayerController _playerController;
    public PlayerCondition _playerCondition;

    private void Awake()
    {
        UnitManager.Instance.Player = this;

        _playerController = GetComponent<PlayerController>();
        _playerCondition = GetComponent<PlayerCondition>();
    }
}
