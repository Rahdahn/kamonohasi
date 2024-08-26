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
        // マウスクリックを検出
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                // クリックしたオブジェクトの位置を取得
                targetPosition = hit.transform.position;
                isZooming = true;
            }
        }

        // ズーム処理
        if (isZooming)
        {
            ZoomToTarget();
        }
    }

    void ZoomToTarget()
    {
        // カメラの位置をクリックされたオブジェクトの位置に移動
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(targetPosition.x, targetPosition.y, mainCamera.transform.position.z), zoomSpeed * Time.deltaTime);

        // カメラのズームを調整
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetOrthographicSize, zoomSpeed * Time.deltaTime);

        // 一定範囲内に収まったらズーム完了
        if (Vector3.Distance(mainCamera.transform.position, new Vector3(targetPosition.x, targetPosition.y, mainCamera.transform.position.z)) < 0.01f &&
            Mathf.Abs(mainCamera.orthographicSize - targetOrthographicSize) < 0.01f)
        {
            isZooming = false;
        }
    }
}
