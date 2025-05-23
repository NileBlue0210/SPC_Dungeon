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

    public TextMeshProUGUI promptText;  // to do: 인스펙터 창에서 직접 끌어오는 것이 아닌, 바로 데이터를 받을 수 있도록 개수해 볼 것
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)  // 매 프레임마다 레이를 쏘는 것을 방지
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, interactableLayer))
            {
                if (hit.collider.gameObject != curInteractGameObject)   // 기존에 상호작용 중인 오브젝트와 다른 오브젝트를 비추었을 때
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();

                    SetPromptText(); // 설명 프롬프트 활성화
                }
            }
            else
            {
                // 빈 공간에 Ray를 쏘았을 때, 기존의 상호작용 정보를 초기화
                curInteractGameObject = null;
                curInteractable = null;

                promptText.gameObject.SetActive(false); // 설명 프롬프트 비활성화
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
            curInteractable.OnInteract();   // to do: 상호작용 후 반드시 오브젝트를 파괴하는 사양 → 조건이 있다면 상호작용 후에도 오브젝트를 유지하도록 해볼 것
            curInteractGameObject = null; // 상호작용 후 오브젝트 초기화
            curInteractable = null; // 상호작용 후 인터페이스 초기화

            promptText.gameObject.SetActive(false);
        }
    }
}
