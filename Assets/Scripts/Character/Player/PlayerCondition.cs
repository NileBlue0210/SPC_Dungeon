using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakePhysicalDamage(float amount);
}

public class PlayerCondition : MonoBehaviour, IDamageable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public event Action onTakeDamage;
    public event Action onTakeBoost;

    void Update()
    {
        // to do: ������ ���¿��� ü���� ȸ���Ǵ� ���� ���� ����
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (health.curValue <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Die()
    {
        Debug.Log("Player died");
    }

    public void TakePhysicalDamage(float amount)
    {
        health.Subtract(amount);

        onTakeDamage.Invoke();
    }

    public void Boost()
    {
        onTakeBoost?.Invoke();
    }
}
