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
                jumpDirection = new Vector3(0, 1, 0); // ���� ������ �������� ���� �� to do: ������Ʈ�� ���� ���� ������ �ٸ��� ������ �� �ֵ��� �غ���

                playerRb.AddForce(jumpDirection * jumpPower, ForceMode.Impulse);
            }
        }
    }
}
