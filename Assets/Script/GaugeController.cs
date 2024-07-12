using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public Slider slider;  // スライダーをアタッチする
    public Button button;  // ボタンをアタッチする
    public float minSpeed = 0.5f;  // 最小速度
    public float maxSpeed = 3.0f;  // 最大速度
    public float successThreshold = 0.8f;  // 成功と判定する基準の数値
    private bool isFilling = true;  // スライダーが動いているかどうか
    private bool isIncreasing = true;  // スライダーの値が増加しているかどうか

    public MovementController[] movementControllers;  // MovementControllerの配列
    public ObjectActivator[] objectActivators;  // ObjectActivatorの配列

    private ActiveManager activeManager;  // ActiveManagerの参照

    void Start()
    {
        button.onClick.AddListener(OnButtonClick);  // ボタンにクリックリスナーを追加

        // ActiveManagerを探す
        activeManager = FindObjectOfType<ActiveManager>();
        if (activeManager == null)
        {
            Debug.LogError("ActiveManager not found in the scene!");
        }

        gameObject.SetActive(false);  // 初期状態を非アクティブにする
    }

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
        }
    }

    void OnButtonClick()
    {
        if (!isFilling) return;  // スライダーが停止している場合は処理しない

        isFilling = false;  // スライダーの動作を停止

        // 判定を行う
        if (slider.value >= successThreshold)
        {
            Debug.Log("Success!");  // 成功した場合の処理
            AwardItem();  // アイテムを獲得する

            foreach (var movementController in movementControllers)
            {
                if (movementController != null)
                {
                    movementController.ResumeMovement();  // 成功時に動きを再開する
                }
            }
        }
        else
        {
            Debug.Log("Failure.");  // 失敗した場合の処理
        }

        slider.gameObject.SetActive(false);  // 判定後にスライダーを非アクティブにする
        button.gameObject.SetActive(false);  // ボタンも非アクティブにする

        foreach (var objectActivator in objectActivators)
        {
            if (objectActivator != null)
            {
                objectActivator.Deactivate();  // 成功または失敗時にオブジェクトを非アクティブにする
            }
        }

        // 自身を非アクティブにする前にActiveManagerに登録
        if (activeManager != null)
        {
            activeManager.RegisterInactiveObject(this);
        }
        gameObject.SetActive(false);
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
    }

    // スライダーとボタンを再アクティブにする関数
    public void ActivateSliderAndButton()
    {
        slider.gameObject.SetActive(true);  // スライダーをアクティブにする
        button.gameObject.SetActive(true);  // ボタンをアクティブにする

        ResetSlider();  // スライダーを初期化
    }

    // オブジェクトを再アクティブ化するための関数
    public void Reactivate()
    {
        gameObject.SetActive(true);  // オブジェクトをアクティブにする
        ActivateSliderAndButton();  // スライダーとボタンを再アクティブにする
    }

    // スライダーを初期化する関数
    private void ResetSlider()
    {
        slider.value = slider.minValue;  // スライダーの値を初期化
        isFilling = true;  // スライダーの動作を開始
        isIncreasing = true;  // スライダーの増加方向を初期化
    }
}
