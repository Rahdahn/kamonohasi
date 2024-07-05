using UnityEngine;
using UnityEngine.UI;  // Button�p�ɒǉ�

public class MovementController : MonoBehaviour
{
    public MonoBehaviour[] controlScripts;  // �����𐧌䂷��S�X�N���v�g�̎Q��
    public ObjectAppearanceController appearanceController; // ObjectAppearanceController�̎Q��
    public Button pauseButton;  // �ꎞ��~�{�^���̎Q��
    private Rigidbody2D rb;
    private bool isPaused = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing from the game object.");
        }

        // �{�^���̃N���b�N�C�x���g�Ƀ��X�i�[��ǉ�
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
            appearanceController.ShowObjects(); // �I�u�W�F�N�g��\��
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
            appearanceController.HideObjects(); // �I�u�W�F�N�g���\��
        }
    }
}
