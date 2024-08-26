using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{
    public MonoBehaviour[] controlScripts;  // 動きを制御する全スクリプトの参照
    public ObjectAppearanceController appearanceController; // ObjectAppearanceControllerの参照
    public Button pauseButton;  // 一時停止ボタンの参照
    public GaugeController gaugeController;  // GaugeControllerの参照
    public Slider gaugeSlider;  // 動かすスライダーの参照
    public Image gaugeImage1;  // 1つ目のImageオブジェクトの参照
    public Image gaugeImage2;  // 2つ目のImageオブジェクトの参照

    private Rigidbody2D rb;
    private bool isPaused = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing from the game object.");
        }

        // ボタンのクリックイベントにリスナーを追加
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePause);
        }
        else
        {
            Debug.LogError("Pause button is not assigned.");
        }

        // 初期状態でゲージとImageを非アクティブにしておく
        if (gaugeController != null)
        {
            gaugeController.gameObject.SetActive(false);
        }

        if (gaugeImage1 != null)
        {
            gaugeImage1.gameObject.SetActive(false);
        }

        if (gaugeImage2 != null)
        {
            gaugeImage2.gameObject.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        TogglePause();
    }

    public void TogglePause()
    {
        if (!isPaused)
        {
            PauseMovement();
        }
        else
        {
            ResumeMovement();
        }
    }

    public void PauseMovement()
    {
        foreach (var script in controlScripts)
        {
            script.enabled = false;
        }
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
        }
        isPaused = true;

        if (appearanceController != null)
        {
            appearanceController.ShowObjects(); // オブジェクトを表示
        }

        // オブジェクトが停止したときにゲージとImageを表示し、指定したスライダーを動かす
        if (gaugeController != null && gaugeSlider != null)
        {
            gaugeController.gameObject.SetActive(true);
            gaugeController.slider = gaugeSlider;  // 指定されたスライダーをセット
            gaugeController.ActivateSliderAndButton();  // スライダーを動かす
        }

        if (gaugeImage1 != null)
        {
            gaugeImage1.gameObject.SetActive(true);
        }

        if (gaugeImage2 != null)
        {
            gaugeImage2.gameObject.SetActive(true);
        }
    }

    public void ResumeMovement()
    {
        foreach (var script in controlScripts)
        {
            script.enabled = true;
        }
        if (rb != null)
        {
            rb.isKinematic = false;
        }
        isPaused = false;

        if (appearanceController != null)
        {
            appearanceController.HideObjects(); // オブジェクトを非表示
        }

        // 動き出したときにゲージとImageを非表示
        if (gaugeController != null)
        {
            gaugeController.gameObject.SetActive(false);
        }

        if (gaugeImage1 != null)
        {
            gaugeImage1.gameObject.SetActive(false);
        }

        if (gaugeImage2 != null)
        {
            gaugeImage2.gameObject.SetActive(false);
        }
    }
}
