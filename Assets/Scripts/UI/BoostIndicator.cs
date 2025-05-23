using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;    // �ν�Ʈ ����Ʈ�� �����̴� �ӵ�
    public BoostObject boostObject;

    private Coroutine coroutine;

    void Start()
    {
        UnitManager.Instance.Player._playerCondition.onTakeBoost += ApplyBoost; // �÷��̾� ������ ��������Ʈ ȣ��
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
        float alertTime = boostObject.boostAmount * ( 2f / 3f );  // �ν�Ʈ �����ð� ���� �˸� ��� �ð�
        float flashDuration = boostObject.boostAmount * ( 1f / 3f ); // �ν�Ʈ �����ð��� 1/3 ������ ���� �����̵��� ����
        float elapsed = 0f; // ���� ��� �ð�

        float a = 90f / 255f;

        yield return new WaitForSeconds(alertTime);

        while (elapsed < flashDuration)
        {
            float alpha = Mathf.PingPong(Time.time * flashSpeed, a);

            image.color = new Color(100f / 255f, 230f / 255f, 255f / 255f, alpha);
            elapsed += Time.deltaTime;

            yield return null;
        }

        image.enabled = false; // �������� ������ �̹��� ��Ȱ��ȭ
    }
}
