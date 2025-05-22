using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpObject : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    private Vector3 jumpDirection;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = UnitManager.Instance.Player._playerController._rb;

            if (playerRb != null)
            {
                jumpDirection = new Vector3(0, 1, 0); // 점프 방향을 위쪽으로 설정 → to do: 오브젝트에 따라 점프 방향을 다르게 설정할 수 있도록 해보자

                playerRb.AddForce(jumpDirection * jumpPower, ForceMode.Impulse);
            }
        }
    }
}
