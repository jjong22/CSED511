using UnityEngine;
using UnityEngine.InputSystem;

public class Teleporter : MonoBehaviour
{
    // public���� �����Ͽ� �ν����� â���� ���� ������ �� �ְ� �մϴ�.
    public LineRenderer lineRenderer;
    public float teleportOffset = 0.5f;

    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // lineRenderer�� ����Ǿ� ���� ���� Ȱ��ȭ/��ġ ����
            if (lineRenderer != null)
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, transform.position); // ������ ������: �÷��̾� ��ġ
                lineRenderer.SetPosition(1, hit.point);         // ������ ����: �浹 ����
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                transform.position = new Vector3(hit.point.x, hit.point.y + teleportOffset, hit.point.z);
            }
        }
        else
        {
            // lineRenderer�� ����Ǿ� ���� ���� ��Ȱ��ȭ
            if (lineRenderer != null)
            {
                lineRenderer.enabled = false;
            }
        }
    }
}