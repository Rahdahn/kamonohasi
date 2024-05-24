using UnityEngine;

public class MovementController : MonoBehaviour
{
    public MonoBehaviour[] controlScripts;  // 動きを制御する全スクリプトの参照
    public ObjectAppearanceController appearanceController; // ObjectAppearanceControllerの参照
    private Rigidbody2D rb;
    private bool isPaused = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
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

    private void PauseMovement()
    {
        foreach (var script in controlScripts)
        {
            script.enabled = false;
        }
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        isPaused = true;

        if (appearanceController != null)
        {
            appearanceController.ShowObjects(); // オブジェクトを表示
        }
    }

    private void ResumeMovement()
    {
        foreach (var script in controlScripts)
        {
            script.enabled = true;
        }
        rb.isKinematic = false;
        isPaused = false;

        if (appearanceController != null)
        {
            appearanceController.HideObjects(); // オブジェクトを非表示
        }
    }
}
