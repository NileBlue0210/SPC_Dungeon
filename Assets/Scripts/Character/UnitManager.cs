using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ ������ ��� ���� �����͸� �����ϴ� �Ŵ���
/// </summary>
public class UnitManager : MonoBehaviour
{
    private static UnitManager _instance;

    public static UnitManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("UnitManager").AddComponent<UnitManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)  // ĳ���� �Ŵ����� ���� ���� ���, �ı� �Ұ� ĳ���� �Ŵ����� ����
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else   // ���� ĳ���� �Ŵ����� �����ϰ� UnitManager�� ��ġ���� ���� ���, �ı��Ѵ�
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
