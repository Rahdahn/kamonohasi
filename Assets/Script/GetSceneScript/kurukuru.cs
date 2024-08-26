using UnityEngine;

public class kurukuru : MonoBehaviour
{
    private Vector3 lastPosition;
    public float minimumMovementThreshold = 0.01f; // この値以上移動した場合にのみ向きを更新

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        float horizontalMove = currentPosition.x - lastPosition.x;

        if (Mathf.Abs(horizontalMove) > minimumMovementThreshold) // ある程度の移動があった場合にのみ実行
        {
            if (horizontalMove > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // 右向き
            }
            else if (horizontalMove < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // 左向き
            }
        }

        lastPosition = currentPosition; // 現在の位置を更新
    }
}
