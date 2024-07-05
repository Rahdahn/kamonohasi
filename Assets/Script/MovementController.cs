using UnityEngine;
using UnityEngine.UI;  // Button用に追加

public class MovementController : MonoBehaviour
{
    public MonoBehaviour[] controlScripts;  // 動きを制御する全スクリプトの参照
    public ObjectAppearanceController appearanceController; // ObjectAppearanceControllerの参照
    public Button pauseButton;  // 一時停止ボタンの参照
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
    }
}
