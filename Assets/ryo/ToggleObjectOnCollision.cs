using UnityEngine;

public class ToggleObjectOnAnyBoxColliderClick : MonoBehaviour
{
    // 出現させる/非アクティブにするオブジェクト
    public GameObject targetObject;

    // 初期状態でのオブジェクトの状態を設定
    private bool isActive = false;

    void Start()
    {
        // ターゲットオブジェクトを非アクティブに設定
        targetObject.SetActive(isActive);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリック
        {
            // マウス位置を取得し、ワールド座標に変換
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // マウス位置にレイを飛ばしてヒットしたオブジェクトを取得
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // クリックされたオブジェクトがBoxCollider2Dを持っているか確認
            if (hit.collider != null && hit.collider.GetComponent<BoxCollider2D>() != null)
            {
                // ターゲットオブジェクトのアクティブ状態を切り替え
                isActive = !isActive;
                targetObject.SetActive(isActive);
            }
        }
    }
}
