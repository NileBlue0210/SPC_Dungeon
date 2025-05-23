using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask interactableLayer;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;  // to do: �ν����� â���� ���� ������� ���� �ƴ�, �ٷ� �����͸� ���� �� �ֵ��� ������ �� ��
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)  // �� �����Ӹ��� ���̸� ��� ���� ����
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, interactableLayer))
            {
                if (hit.collider.gameObject != curInteractGameObject)   // ������ ��ȣ�ۿ� ���� ������Ʈ�� �ٸ� ������Ʈ�� ���߾��� ��
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();

                    SetPromptText(); // ���� ������Ʈ Ȱ��ȭ
                }
            }
            else
            {
                // �� ������ Ray�� ����� ��, ������ ��ȣ�ۿ� ������ �ʱ�ȭ
                curInteractGameObject = null;
                curInteractable = null;

                promptText.gameObject.SetActive(false); // ���� ������Ʈ ��Ȱ��ȭ
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();   // to do: ��ȣ�ۿ� �� �ݵ�� ������Ʈ�� �ı��ϴ� ��� �� ������ �ִٸ� ��ȣ�ۿ� �Ŀ��� ������Ʈ�� �����ϵ��� �غ� ��
            curInteractGameObject = null; // ��ȣ�ۿ� �� ������Ʈ �ʱ�ȭ
            curInteractable = null; // ��ȣ�ۿ� �� �������̽� �ʱ�ȭ

            promptText.gameObject.SetActive(false);
        }
    }
}
