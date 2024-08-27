using System;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    public float moveSpeed = 2f;        // 動物の移動速度
    public float changeDirectionTime = 2f; // 方向を変えるまでの時間

    private Vector2 targetPosition;    // 動物が移動する目標地点
    private float timer;               // 方向変更のタイマー
    private bool isMoving = true;      // 移動状態を制御するフラグ

    private void Start()
    {
        SetRandomTargetPosition(); // 初期の目標地点を設定
    }

    private void Update()
    {
        if (isMoving)
        {
            // タイマーを更新し、一定時間経過後に方向を変える
            timer += Time.deltaTime;
            if (timer >= changeDirectionTime)
            {
                SetRandomTargetPosition();
                timer = 0;
            }

            // 動物を目標地点に向かって移動させる
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 目標地点に到達したら、新しい目標地点を設定する
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                SetRandomTargetPosition();
            }
        }
    }

    private void SetRandomTargetPosition()
    {
        // カメラのビューポートサイズを取得
        Camera cam = Camera.main;
        Vector2 minBounds = cam.ViewportToWorldPoint(new Vector2(0.1f, 0.1f)); // 左下のワールド座標
        Vector2 maxBounds = cam.ViewportToWorldPoint(new Vector2(0.9f, 0.9f)); // 右上のワールド座標

        // ビューポート内でランダムな目標地点を設定
        targetPosition = new Vector2(
            UnityEngine.Random.Range(minBounds.x, maxBounds.x),
            UnityEngine.Random.Range(minBounds.y, maxBounds.y)
        );
    }

    // 移動を停止するメソッド
    public void StopMoving()
    {
        isMoving = false;
    }

    // 移動を再開するメソッド
    public void StartMoving()
    {
        isMoving = true;
    }
}
