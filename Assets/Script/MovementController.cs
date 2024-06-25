using UnityEngine;
using UnityEngine.UI; // �{�^���p�ɒǉ�

public class MovementController : MonoBehaviour
{
    public MonoBehaviour[] controlScripts;  // �����𐧌䂷��S�X�N���v�g�̎Q��
    public ObjectAppearanceController appearanceController; // ObjectAppearanceController�̎Q��
    private Rigidbody2D rb;
    private bool isPaused = false;

    public Button pauseButton; // �{�^���̎Q��

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // �{�^���̃N���b�N�C�x���g�Ƀ��\�b�h��o�^
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePause);
        }
    }

    void OnMouseDown()
    {
        TogglePause();
    }

    private void TogglePause()
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
