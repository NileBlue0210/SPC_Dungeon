using System;
using UnityEngine;


/// <summary>
/// 플레이어 오브젝트의 시그니처 클래스
/// </summary>
public class Player : MonoBehaviour
{
    public PlayerController _playerController;
    public PlayerCondition _playerCondition;

    public ItemData itemData;
    public Action addItem;

    private void Awake()
    {
        UnitManager.Instance.Player = this;

        _playerController = GetComponent<PlayerController>();
        _playerCondition = GetComponent<PlayerCondition>();
    }
}
