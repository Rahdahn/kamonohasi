using System.Collections;
using UnityEngine;

public class AnimalMover : MonoBehaviour
{
    public Transform startLocation;
    public Transform goalLocation;
    public Transform[] relayPoints;
    public float speed = 5f;
    public float cooldownTime = 2f;

    // リスポーン対象のオブジェクトを指定するフィールド
    public Transform[] objectsToRespawn;

    private Transform target;
    private int currentRelayPointIndex = 0;
    private bool isMoving = true;

    void Start()
    {
        // 最初の目標をスタート地点に設定
        target = startLocation;
    }

    void Update()
    {
        if (!isMoving)
        {
            StartCoroutine(RespawnAfterCooldown()); // 移動が終了している場合はリスポーン処理を開始
            return;
        }

        // 目標に向かって移動
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // 目標に到達したら次の目標を設定
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            if (target == startLocation)
            {
                // スタート地点に到達したら中継地点に切り替え
                if (relayPoints.Length > 0)
                {
                    currentRelayPointIndex = 0;
                    target = relayPoints[currentRelayPointIndex];
                }
                else
                {
                    target = goalLocation;
                }
            }
            else if (target == goalLocation)
            {
                // ゴール地点に到達したら移動を終了
                isMoving = false;
                StartCoroutine(RespawnAfterCooldown());
            }
            else if (ArrayIsValidIndex(currentRelayPointIndex, relayPoints))
            {
                // 中継地点に到達したら次の中継地点に切り替え（最後の中継地点の場合はゴール地点に切り替え）
                currentRelayPointIndex++;
                if (ArrayIsValidIndex(currentRelayPointIndex, relayPoints))
                {
                    target = relayPoints[currentRelayPointIndex];
                }
                else
                {
                    target = goalLocation;
                }
            }
        }
    }

    // 配列の有効なインデックスかどうかを確認
    bool ArrayIsValidIndex(int index, Transform[] array)
    {
        return index >= 0 && index < array.Length;
    }

    // リスポーン処理を開始するコルーチン
    IEnumerator RespawnAfterCooldown()
    {
        // オブジェクトの動きを一時的に停止
        foreach (var obj in objectsToRespawn)
        {
            if (obj != null)
            {
                // 動きを停止するための処理を追加（例: 速度を0にするなど）
                var mover = obj.GetComponent<AnimalMover>();
                if (mover != null)
                {
                    mover.StopMoving(); // 停止メソッドを呼び出す
                }
            }
        }

        // クールダウンの待機
        yield return new WaitForSeconds(cooldownTime);

        // 指定されたオブジェクトを非アクティブにして初期位置に戻す
        foreach (var obj in objectsToRespawn)
        {
            if (obj != null)
            {
                obj.gameObject.SetActive(false);
                obj.position = startLocation.position;
                obj.gameObject.SetActive(true);

                // 動きを再開するための処理を追加（例: 速度を元に戻すなど）
                var mover = obj.GetComponent<AnimalMover>();
                if (mover != null)
                {
                    mover.StartMoving(); // 再開メソッドを呼び出す
                }
            }
        }

        // オブジェクトの動きを再開
        isMoving = true;
        currentRelayPointIndex = 0;
        target = startLocation;
    }

    // 成功判定を受け取った際に呼ばれるメソッド
    public void OnGaugeSuccess()
    {
        // 移動を停止
        StopMoving();

        // リスポーン処理を開始
        StartCoroutine(RespawnAfterCooldown());
    }

    // オブジェクトの動きを停止するメソッド
    public void StopMoving()
    {
        isMoving = false; // 移動フラグを停止
        speed = 0f; // 動きを停止する
    }

    // オブジェクトの動きを再開するメソッド
    public void StartMoving()
    {
        isMoving = true; // 移動フラグを再開
        speed = 5f; // 元の速度に戻す
    }
}
