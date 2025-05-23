using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostObject : ItemObject
{
    private float normalSpeed;
    private float normalJumpForce;
    public float AddSpeedRate;
    public float AddJumpForceRate;
    public float boostAmount;

    private Coroutine coroutine;

    private void Boost()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        normalSpeed = UnitManager.Instance.Player._playerController.moveSpeed;
        normalJumpForce = UnitManager.Instance.Player._playerController.jumpForce;

        UnitManager.Instance.Player._playerController.moveSpeed *= AddSpeedRate;
        UnitManager.Instance.Player._playerController.jumpForce *= AddJumpForceRate;

        coroutine = StartCoroutine(BoostEnd());
    }

    private IEnumerator BoostEnd()
    {
        yield return new WaitForSeconds(boostAmount);

        // issue: 여러번 가속을 적용할 경우 가속이 누적되는 문제가 발생 중
        UnitManager.Instance.Player._playerController.moveSpeed = normalSpeed;
        UnitManager.Instance.Player._playerController.jumpForce = normalJumpForce;

        yield return null;
    }

    public override void OnInteract()
    {
        base.OnInteract();

        Boost();

        UnitManager.Instance.Player._playerCondition.Boost();
    }
}
