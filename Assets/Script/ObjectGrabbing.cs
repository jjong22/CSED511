using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectGrabbingWithHighlight : MonoBehaviour
{
    public float grabDistance = 2f;
    public float holdDistance = 1.5f;
    public float lerpFactor = 0.2f;

    private Rigidbody grabbedBody;
    private Renderer highlightedRenderer;
    private Color originalColor;

    void Update()
    {
        // 레이 생성
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        // Debug 레이
        Debug.DrawRay(ray.origin, ray.direction * grabDistance, Color.red, 0.1f);

        // 이전 하이라이트 초기화
        if (highlightedRenderer != null)
        {
            highlightedRenderer.material.color = originalColor;
            highlightedRenderer = null;
        }

        // 레이캐스트 검사
        if (Physics.Raycast(ray, out hit, grabDistance))
        {
            if (hit.rigidbody != null && hit.collider.gameObject.CompareTag("Grabbable"))
            {
                // 하이라이트 적용
                highlightedRenderer = hit.rigidbody.GetComponent<Renderer>();
                if (highlightedRenderer != null)
                {
                    originalColor = highlightedRenderer.material.color;
                    highlightedRenderer.material.color = Color.red;
                }

                // 마우스 클릭하면 잡기
                if (Mouse.current.rightButton.wasPressedThisFrame && grabbedBody == null)
                {
                    grabbedBody = hit.rigidbody;
                    grabbedBody.isKinematic = true;
                }
            }
        }

        // 놓기
        if (Mouse.current.rightButton.wasReleasedThisFrame && grabbedBody != null)
        {
            grabbedBody.isKinematic = false;
            grabbedBody = null;
        }
    }

    void FixedUpdate()
    {
        // 잡은 물체 이동
        if (grabbedBody != null)
        {
            Vector3 targetPos = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;
            grabbedBody.MovePosition(Vector3.Lerp(grabbedBody.position, targetPos, lerpFactor));
        }
    }
}
