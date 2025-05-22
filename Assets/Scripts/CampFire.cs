using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public int damage;
    public float damageRate;

    List<IDamageable> things = new List<IDamageable>(); // 복수의 피격자가 존재할 경우, List로 피격자를 관리

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))   // 트리거를 발동시킨 오브젝트가 IDamageable, 즉 데미지를 입는 오브젝트일 경우
        {
            things.Add(damageable); // 리스트에 추가
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            things.Remove(damageable);
        }
    }
}
