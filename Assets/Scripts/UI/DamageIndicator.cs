using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;    // 데미지 이펙트의 깜빡이는 속도

    private Coroutine coroutine;

    void Start()
    {
        UnitManager.Instance.Player._playerCondition.onTakeDamage += Flash; // 플레이어 데미지 델리게이트 호출
    }

    public void Flash()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        image.enabled = true;
        image.color = new Color(1f, 100f / 255f, 100f / 255f);

        coroutine = StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway()
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while (a > 0)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(1f, 100f / 255f, 100f / 255f, a);

            yield return null;
        }

        image.enabled = false; // 깜빡임이 끝나면 이미지 비활성화
    }
}
