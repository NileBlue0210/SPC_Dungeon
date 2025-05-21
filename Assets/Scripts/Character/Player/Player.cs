using UnityEngine;


/// <summary>
/// 플레이어 오브젝트의 시그니처 클래스
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
