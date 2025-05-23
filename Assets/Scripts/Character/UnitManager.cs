using UnityEngine;

/// <summary>
/// 플레이어를 포함한 모든 유닛 데이터를 관리하는 매니저
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

    private Player _player;
    public Player Player { get { return _player; } set {  _player = value; } }

    private void Awake()
    {
        if (_instance == null)  // 캐릭터 매니저가 씬에 없을 경우, 파괴 불가 캐릭터 매니저를 생성
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else   // 기존 캐릭터 매니저가 존재하고 UnitManager와 일치하지 않을 경우, 파괴한다
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
