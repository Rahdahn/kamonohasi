using System.Collections;
using UnityEngine;

public class RandomMover : MonoBehaviour
{
    public GameObject targetObject; // 移動するオブジェクトを指定する
    public float moveRangeX = 5.0f; // 横方向の移動範囲
    public float moveRangeY = 5.0f; // 縦方向の移動範囲
    public float moveInterval = 2.0f; // ランダムに移動する間隔（秒）
    public float respawnDelay = 3.0f; // リスポーンするまでの時間
    public float moveSpeed = 2.0f; // 移動速度

    public Vector2 gizmoOffset = new Vector2(0, 0); // Gizmos 表示のオフセット
    public Vector2 offScreenOffset = new Vector2(1000, 1000); // 画面外に移動するオフセット

    private Vector2 originalPosition;
    private Vector2 targetPosition;
    private bool isPaused = false; // 一時停止状態を保持するフラグ
    private bool isMoving = false;

    void Start()
    {
        if (targetObject != null)
        {
            originalPosition = targetObject.transform.position;
            StartCoroutine(SetupMovement()); // 初期化と移動設定
        }
        else
        {
            Debug.LogError("Target Object が指定されていません。");
        }
    }

    void Update()
    {
        if (isPaused || !isMoving) return;

        // スムーズに移動
        targetObject.transform.position = Vector2.MoveTowards(targetObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // 目標位置に到達したら次の目標位置を設定
        if ((Vector2)targetObject.transform.position == targetPosition)
        {
            StartCoroutine(SetRandomTargetPosition());
        }
    }

    IEnumerator SetupMovement()
    {
        while (true)
        {
            if (targetObject != null && !isPaused)
            {
                targetPosition = GetRandomPosition();
                isMoving = true;
                yield return new WaitForSeconds(moveInterval);
            }
            else
            {
                yield return null;
            }
        }
    }

    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(-moveRangeX, moveRangeX);
        float randomY = Random.Range(-moveRangeY, moveRangeY);
        return new Vector2(originalPosition.x + randomX, originalPosition.y + randomY);
    }

    IEnumerator SetRandomTargetPosition()
    {
        if (!isPaused)
        {
            targetPosition = GetRandomPosition();
        }
        yield return null;
    }

    public void OnSuccess()
    {
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        if (targetObject != null)
        {
            isMoving = false;
            Debug.Log("ターゲットオブジェクトが画面外に移動しました。");

            Vector2 offScreenPosition = (Vector2)targetObject.transform.position + offScreenOffset;
            targetObject.transform.position = offScreenPosition;

            yield return new WaitForSeconds(respawnDelay);

            Debug.Log("ターゲットオブジェクトが元の位置に戻りました。");
            targetObject.transform.position = originalPosition;
            StartCoroutine(SetupMovement());
        }
    }

    public void PauseMovement()
    {
        isPaused = true;
        isMoving = false;
        Debug.Log("移動が一時停止されました。");
    }

    public void ResumeMovement()
    {
        isPaused = false;
        isMoving = true;
        Debug.Log("移動が再開されました。");
        StartCoroutine(SetupMovement()); // 再び移動を開始
    }

    // マウスクリックを検知してポーズ状態を切り替える
    void OnMouseDown()
    {
        if (isPaused)
        {
            ResumeMovement();
        }
        else
        {
            PauseMovement();
        }
    }

    void OnDrawGizmos()
    {
        if (targetObject != null)
        {
            Gizmos.color = Color.green;
            Vector3 gizmoCenter = new Vector3(originalPosition.x, originalPosition.y, 0) + new Vector3(gizmoOffset.x, gizmoOffset.y, 0);
            Gizmos.DrawWireCube(gizmoCenter, new Vector3(moveRangeX * 2, moveRangeY * 2, 0));
        }
    }
}
