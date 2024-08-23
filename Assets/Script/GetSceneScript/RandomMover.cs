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
            Debug.LogError("Target Object is not assigned in the Inspector.");
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

    // 初期設定と移動を設定するコルーチン
    IEnumerator SetupMovement()
    {
        while (true)
        {
            if (targetObject != null)
            {
                targetPosition = GetRandomPosition();
                isMoving = true;
                yield return new WaitForSeconds(moveInterval);
            }
            else
            {
                yield break; // targetObject が null の場合、コルーチンを終了
            }
        }
    }

    // ランダムな目標位置を取得
    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(-moveRangeX, moveRangeX);
        float randomY = Random.Range(-moveRangeY, moveRangeY);
        return new Vector2(originalPosition.x + randomX, originalPosition.y + randomY);
    }

    // 目標位置を設定するコルーチン
    IEnumerator SetRandomTargetPosition()
    {
        targetPosition = GetRandomPosition();
        yield return null; // 次のフレームでの移動開始
    }

    // 他のスクリプトから呼び出される成功判定を受け取るメソッド
    public void OnSuccess()
    {
        StartCoroutine(Respawn());
    }

    // オブジェクトを画面外に移動させ、一定時間後にリスポーンさせるコルーチン
    IEnumerator Respawn()
    {
        if (targetObject != null)
        {
            // 一時停止する
            isMoving = false;
            Debug.Log("Target object moved off-screen.");

            // 画面外に移動させる
            Vector2 offScreenPosition = (Vector2)targetObject.transform.position + offScreenOffset;
            targetObject.transform.position = offScreenPosition;

            // リスポーンまで待機
            yield return new WaitForSeconds(respawnDelay);

            // リスポーン処理
            Debug.Log("Target object moved back to the original position.");

            // 元の位置に戻す
            targetObject.transform.position = originalPosition;
            StartCoroutine(SetupMovement()); // 移動再開
        }
    }

    // 一時停止機能を連動させるためのメソッド
    public void PauseMovement()
    {
        isPaused = true;
        Debug.Log("Movement paused.");
    }

    public void ResumeMovement()
    {
        isPaused = false;
        Debug.Log("Movement resumed.");
    }

    // Gizmos を使用して移動範囲を表示
    void OnDrawGizmos()
    {
        if (targetObject != null)
        {
            // 移動範囲の中心位置を表示
            Gizmos.color = Color.green;
            Vector3 gizmoCenter = new Vector3(originalPosition.x, originalPosition.y, 0) + new Vector3(gizmoOffset.x, gizmoOffset.y, 0);
            Gizmos.DrawWireCube(gizmoCenter, new Vector3(moveRangeX * 2, moveRangeY * 2, 0));
        }
    }
}
