using UnityEngine;

public class ClickZoom : MonoBehaviour
{
    public Camera mainCamera;
    public float zoomSpeed = 2f;
    public float targetOrthographicSize = 2f;
    private Vector3 targetPosition;
    private bool isZooming = false;

    void Update()
    {
        // �}�E�X�N���b�N�����o
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                // �N���b�N�����I�u�W�F�N�g�̈ʒu���擾
                targetPosition = hit.transform.position;
                isZooming = true;
            }
        }

        // �Y�[������
        if (isZooming)
        {
            ZoomToTarget();
        }
    }

    void ZoomToTarget()
    {
        // �J�����̈ʒu���N���b�N���ꂽ�I�u�W�F�N�g�̈ʒu�Ɉړ�
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(targetPosition.x, targetPosition.y, mainCamera.transform.position.z), zoomSpeed * Time.deltaTime);

        // �J�����̃Y�[���𒲐�
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetOrthographicSize, zoomSpeed * Time.deltaTime);

        // ���͈͓��Ɏ��܂�����Y�[������
        if (Vector3.Distance(mainCamera.transform.position, new Vector3(targetPosition.x, targetPosition.y, mainCamera.transform.position.z)) < 0.01f &&
            Mathf.Abs(mainCamera.orthographicSize - targetOrthographicSize) < 0.01f)
        {
            isZooming = false;
        }
    }
}
