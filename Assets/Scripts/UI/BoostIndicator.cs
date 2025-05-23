using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;    // 부스트 이펙트의 깜빡이는 속도
    public BoostObject boostObject;

    private Coroutine coroutine;

    void Start()
    {
        UnitManager.Instance.Player._playerCondition.onTakeBoost += ApplyBoost; // 플레이어 데미지 델리게이트 호출
    }

    public void ApplyBoost()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        image.enabled = true;
        image.color = new Color(100f / 255f, 230f / 255f, 255f / 255f, 90f/ 255f);

        coroutine = StartCoroutine(AlertBoostEnd());
    }

    private IEnumerator AlertBoostEnd()
    {
        float alertTime = boostObject.boostAmount * ( 2f / 3f );  // 부스트 유지시간 종료 알림 대기 시간
        float flashDuration = boostObject.boostAmount * ( 1f / 3f ); // 부스트 유지시간이 1/3 이하일 동안 깜빡이도록 설정
        float elapsed = 0f; // 현재 경과 시간

        float a = 90f / 255f;

        yield return new WaitForSeconds(alertTime);

        while (elapsed < flashDuration)
        {
            float alpha = Mathf.PingPong(Time.time * flashSpeed, a);

            image.color = new Color(100f / 255f, 230f / 255f, 255f / 255f, alpha);
            elapsed += Time.deltaTime;

            yield return null;
        }

        image.enabled = false; // 깜빡임이 끝나면 이미지 비활성화
    }
}
