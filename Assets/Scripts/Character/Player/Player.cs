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
        UnitManager.Instance.Player = this;

        _playerController = GetComponent<PlayerController>();
        _playerCondition = GetComponent<PlayerCondition>();
    }
}
