using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public Vector2 moveableAreaSize = new Vector2(5f, 3f); // 移動可能な範囲のサイズ

    private Vector2 minPosition; // 最小の移動可能位置
    private Vector2 maxPosition; // 最大の移動可能位置

    void Start()
    {
        // 最小と最大の位置を計算する
        minPosition = new Vector2(transform.position.x - moveableAreaSize.x / 2f, transform.position.y - moveableAreaSize.y / 2f);
        maxPosition = new Vector2(transform.position.x + moveableAreaSize.x / 2f, transform.position.y + moveableAreaSize.y / 2f);
    }

    void OnMouseDown()
    {
        // ドラッグを開始する際に必要な処理を追加
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        // 移動可能な範囲内でのみオブジェクトを移動させる
        float clampedX = Mathf.Clamp(mousePosition.x, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(mousePosition.y, minPosition.y, maxPosition.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    void OnMouseUp()
    {
        // ドラッグを終了する際に必要な処理を追加
    }

    // 範囲のサイズが変更された場合に呼び出される処理
    void OnValidate()
    {
        // 最小と最大の位置を再計算する
        minPosition = new Vector2(transform.position.x - moveableAreaSize.x / 2f, transform.position.y - moveableAreaSize.y / 2f);
        maxPosition = new Vector2(transform.position.x + moveableAreaSize.x / 2f, transform.position.y + moveableAreaSize.y / 2f);
    }

    // 移動可能な範囲を示すために Gizmos を使用
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(moveableAreaSize.x, moveableAreaSize.y, 0f));
    }
}
