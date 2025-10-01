using UnityEngine;
using UnityEngine.InputSystem;

public class Teleporter : MonoBehaviour
{
    // public으로 선언하여 인스펙터 창에서 직접 연결할 수 있게 합니다.
    public LineRenderer lineRenderer;
    public float teleportOffset = 0.5f;

    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // lineRenderer가 연결되어 있을 때만 활성화/위치 설정
            if (lineRenderer != null)
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, transform.position); // 레이저 시작점: 플레이어 위치
                lineRenderer.SetPosition(1, hit.point);         // 레이저 끝점: 충돌 지점
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                transform.position = new Vector3(hit.point.x, hit.point.y + teleportOffset, hit.point.z);
            }
        }
        else
        {
            // lineRenderer가 연결되어 있을 때만 비활성화
            if (lineRenderer != null)
            {
                lineRenderer.enabled = false;
            }
        }
    }
}