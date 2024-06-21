using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public Slider slider;  // スライダーをアタッチする
    public float minSpeed = 0.5f;  // 最小速度
    public float maxSpeed = 3.0f;  // 最大速度
    public float successThreshold = 0.8f;  // 成功と判定する基準の数値
    private bool isFilling = true;  // スライダーが動いているかどうか
    private bool isIncreasing = true;  // スライダーの値が増加しているかどうか
    private GameObject lastClickedObject;  // 最後にタップされたオブジェクトを記録する
    private int judgeCount = 0;  // 判定回数を追跡する変数

    void Update()
    {
        if (slider.gameObject.activeInHierarchy)
        {
            if (isFilling)
            {
                float speed = Mathf.Lerp(minSpeed, maxSpeed, slider.value);  // スライダーの値に基づいて速度を調整

                if (isIncreasing)
                {
                    slider.value += speed * Time.deltaTime;
                    if (slider.value >= slider.maxValue)
                    {
                        isIncreasing = false;  // 最大値に達したら減少に切り替える
                    }
                }
                else
                {
                    slider.value -= speed * Time.deltaTime;
                    if (slider.value <= slider.minValue)
                    {
                        isIncreasing = true;  // 最小値に達したら増加に切り替える
                    }
                }
            }

            // タップされたオブジェクトを記録
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                             Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);
                if (hit && hit.collider != null)
                {
                    lastClickedObject = hit.collider.gameObject;
                }
            }

            // 左クリックがあり、記録されたオブジェクトがアクティブな場合にのみ操作を受け付ける
            if (Input.GetMouseButtonDown(0) && lastClickedObject != null && lastClickedObject.activeInHierarchy)
            {
                isFilling = !isFilling;  // 左クリックで動作の停止と再開を切り替える

                // 2回目の判定で成功か失敗の判定を行う
                if (!isFilling)
                {
                    judgeCount++;  // 判定回数を増加させる

                    if (judgeCount == 2)
                    {
                        if (slider.value >= successThreshold)
                        {
                            Debug.Log("Success!");  // 成功した場合の処理
                            AwardItem();  // アイテムを獲得する
                            lastClickedObject.SetActive(false);  // 成功時にオブジェクトを非アクティブにする
                        }
                        else
                        {
                            Debug.Log("Failure.");  // 失敗した場合の処理
                        }

                        slider.gameObject.SetActive(false);  // 2回目の判定でスライダーを非アクティブにする
                    }
                }
            }
        }
    }

    // アイテムを獲得するための関数
    void AwardItem()
    {
        // ここにアイテムを獲得する処理を追加
        // 例えば、インベントリにアイテムを追加する、ポイントを増やすなど
        Debug.Log("Item awarded!");
    }

    // スライダーがアクティブになった時に初期化して動き出す
    void OnEnable()
    {
        slider.value = slider.minValue;  // スライダーの値を初期化
        isFilling = true;  // スライダーの動作を開始
        isIncreasing = true;  // スライダーの増加方向を初期化
        judgeCount = 0;  // 判定回数をリセット
    }
}
