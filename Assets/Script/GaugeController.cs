using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public Slider slider;  // �X���C�_�[���A�^�b�`����
    public Button button;  // �{�^�����A�^�b�`����
    public float minSpeed = 0.5f;  // �ŏ����x
    public float maxSpeed = 3.0f;  // �ő呬�x
    public float successThreshold = 0.8f;  // �����Ɣ��肷���̐��l
    private bool isFilling = true;  // �X���C�_�[�������Ă��邩�ǂ���
    private bool isIncreasing = true;  // �X���C�_�[�̒l���������Ă��邩�ǂ���

    public MovementController[] movementControllers;  // MovementController�̔z��
    public ObjectActivator[] objectActivators;  // ObjectActivator�̔z��

    private ActiveManager activeManager;  // ActiveManager�̎Q��

    void Start()
    {
        button.onClick.AddListener(OnButtonClick);  // �{�^���ɃN���b�N���X�i�[��ǉ�

        // ActiveManager��T��
        activeManager = FindObjectOfType<ActiveManager>();
        if (activeManager == null)
        {
            Debug.LogError("ActiveManager not found in the scene!");
        }

        gameObject.SetActive(false);  // ������Ԃ��A�N�e�B�u�ɂ���
    }

    void Update()
    {
        if (slider.gameObject.activeInHierarchy)
        {
            if (isFilling)
            {
                float speed = Mathf.Lerp(minSpeed, maxSpeed, slider.value);  // �X���C�_�[�̒l�Ɋ�Â��đ��x�𒲐�

                if (isIncreasing)
                {
                    slider.value += speed * Time.deltaTime;
                    if (slider.value >= slider.maxValue)
                    {
                        isIncreasing = false;  // �ő�l�ɒB�����猸���ɐ؂�ւ���
                    }
                }
                else
                {
                    slider.value -= speed * Time.deltaTime;
                    if (slider.value <= slider.minValue)
                    {
                        isIncreasing = true;  // �ŏ��l�ɒB�����瑝���ɐ؂�ւ���
                    }
                }
            }
        }
    }

    void OnButtonClick()
    {
        if (!isFilling) return;  // �X���C�_�[����~���Ă���ꍇ�͏������Ȃ�

        isFilling = false;  // �X���C�_�[�̓�����~

        // ������s��
        if (slider.value >= successThreshold)
        {
            Debug.Log("Success!");  // ���������ꍇ�̏���
            AwardItem();  // �A�C�e�����l������

            foreach (var movementController in movementControllers)
            {
                if (movementController != null)
                {
                    movementController.ResumeMovement();  // �������ɓ������ĊJ����
                }
            }
        }
        else
        {
            Debug.Log("Failure.");  // ���s�����ꍇ�̏���
        }

        slider.gameObject.SetActive(false);  // �����ɃX���C�_�[���A�N�e�B�u�ɂ���
        button.gameObject.SetActive(false);  // �{�^������A�N�e�B�u�ɂ���

        foreach (var objectActivator in objectActivators)
        {
            if (objectActivator != null)
            {
                objectActivator.Deactivate();  // �����܂��͎��s���ɃI�u�W�F�N�g���A�N�e�B�u�ɂ���
            }
        }

        // ���g���A�N�e�B�u�ɂ���O��ActiveManager�ɓo�^
        if (activeManager != null)
        {
            activeManager.RegisterInactiveObject(this);
        }
        gameObject.SetActive(false);
    }

    // �A�C�e�����l�����邽�߂̊֐�
    void AwardItem()
    {
        // �����ɃA�C�e�����l�����鏈����ǉ�
        // �Ⴆ�΁A�C���x���g���ɃA�C�e����ǉ�����A�|�C���g�𑝂₷�Ȃ�
        Debug.Log("Item awarded!");
    }

    // �X���C�_�[���A�N�e�B�u�ɂȂ������ɏ��������ē����o��
    void OnEnable()
    {
        slider.value = slider.minValue;  // �X���C�_�[�̒l��������
        isFilling = true;  // �X���C�_�[�̓�����J�n
        isIncreasing = true;  // �X���C�_�[�̑���������������
    }

    // �X���C�_�[�ƃ{�^�����ăA�N�e�B�u�ɂ���֐�
    public void ActivateSliderAndButton()
    {
        slider.gameObject.SetActive(true);  // �X���C�_�[���A�N�e�B�u�ɂ���
        button.gameObject.SetActive(true);  // �{�^�����A�N�e�B�u�ɂ���

        ResetSlider();  // �X���C�_�[��������
    }

    // �I�u�W�F�N�g���ăA�N�e�B�u�����邽�߂̊֐�
    public void Reactivate()
    {
        gameObject.SetActive(true);  // �I�u�W�F�N�g���A�N�e�B�u�ɂ���
        ActivateSliderAndButton();  // �X���C�_�[�ƃ{�^�����ăA�N�e�B�u�ɂ���
    }

    // �X���C�_�[������������֐�
    private void ResetSlider()
    {
        slider.value = slider.minValue;  // �X���C�_�[�̒l��������
        isFilling = true;  // �X���C�_�[�̓�����J�n
        isIncreasing = true;  // �X���C�_�[�̑���������������
    }
}
