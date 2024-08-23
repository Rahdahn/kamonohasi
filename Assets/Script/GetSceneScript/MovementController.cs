using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{
    public MonoBehaviour[] controlScripts;  // �����𐧌䂷��S�X�N���v�g�̎Q��
    public ObjectAppearanceController appearanceController; // ObjectAppearanceController�̎Q��
    public Button pauseButton;  // �ꎞ��~�{�^���̎Q��
    public GaugeController gaugeController;  // GaugeController�̎Q��
    public Slider gaugeSlider;  // �������X���C�_�[�̎Q��
    public Image gaugeImage1;  // 1�ڂ�Image�I�u�W�F�N�g�̎Q��
    public Image gaugeImage2;  // 2�ڂ�Image�I�u�W�F�N�g�̎Q��

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

        // ������ԂŃQ�[�W��Image���A�N�e�B�u�ɂ��Ă���
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
            appearanceController.ShowObjects(); // �I�u�W�F�N�g��\��
        }

        // �I�u�W�F�N�g����~�����Ƃ��ɃQ�[�W��Image��\�����A�w�肵���X���C�_�[�𓮂���
        if (gaugeController != null && gaugeSlider != null)
        {
            gaugeController.gameObject.SetActive(true);
            gaugeController.slider = gaugeSlider;  // �w�肳�ꂽ�X���C�_�[���Z�b�g
            gaugeController.ActivateSliderAndButton();  // �X���C�_�[�𓮂���
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
            appearanceController.HideObjects(); // �I�u�W�F�N�g���\��
        }

        // �����o�����Ƃ��ɃQ�[�W��Image���\��
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
