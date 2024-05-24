using UnityEngine;

public class MovementController : MonoBehaviour
{
    public MonoBehaviour[] controlScripts;  // �����𐧌䂷��S�X�N���v�g�̎Q��
    public ObjectAppearanceController appearanceController; // ObjectAppearanceController�̎Q��
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
            appearanceController.ShowObjects(); // �I�u�W�F�N�g��\��
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
            appearanceController.HideObjects(); // �I�u�W�F�N�g���\��
        }
    }
}
