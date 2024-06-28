using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MixGaugeManager : MonoBehaviour
{
    [SerializeField] List<Slider> sliders; // �X���C�_�[�̃��X�g
    [SerializeField] UnityEngine.UI.Image successRangeImage; // �����͈͂������摜

    private int currentSliderIndex = 0; // ���݂̃X���C�_�[�̃C���f�b�N�X
    private bool isClicked = false; // �N���b�N��Ԃ�ێ�����t���O
    private bool maxValue = false; // �X���C�_�[�̒l���ő傩�ǂ����������t���O
    private int successPosition; // �����ʒu
    private int minSuccessPosition; // �����ʒu�̍ŏ��l
    private int maxSuccessPosition; // �����ʒu�̍ő�l

    void Start()
    {
        Initialize();
    }

    // ���������\�b�h
    public void Initialize()
    {
        // �S�ẴX���C�_�[��������Ԃɐݒ�
        foreach (Slider slider in sliders)
        {
            slider.value = 0;
            slider.gameObject.SetActive(false);
        }

        // �ŏ��̃X���C�_�[���A�N�e�B�u�ɂ���
        if (sliders.Count > 0)
        {
            sliders[0].gameObject.SetActive(true);
        }

        maxValue = false;
        isClicked = false;
        currentSliderIndex = 0;
        successRangeImage.gameObject.SetActive(false);

        // �ŏ��̐����ʒu��ݒ肵�A�摜��\��
        if (!successRangeImage.gameObject.activeSelf) // �����͈͉摜����A�N�e�B�u�̏ꍇ�̂�
        {
            SetSuccessPositionAndShowImage();
        }
    }

    void Update()
    {
        // �}�E�X�{�^���������ꂽ�Ƃ��̏���
        if (Input.GetMouseButtonDown(0) && !isClicked)
        {
            isClicked = true;

            // �X�g�b�v�����^�C�~���O�Ŕ���
            if (currentSliderIndex < sliders.Count)
            {
                Slider currentSlider = sliders[currentSliderIndex];

                // �����͈͓����ǂ����𔻒�
                if (currentSlider.value >= minSuccessPosition && currentSlider.value <= maxSuccessPosition)
                {
                    currentSlider.gameObject.SetActive(false); // ���݂̃X���C�_�[���A�N�e�B�u�ɂ���
                    successRangeImage.gameObject.SetActive(false); // �����͈͂̉摜���\���ɂ���
                    currentSliderIndex++;

                    if (currentSliderIndex < sliders.Count)
                    {
                        Slider nextSlider = sliders[currentSliderIndex];
                        nextSlider.gameObject.SetActive(true); // ���̃X���C�_�[���A�N�e�B�u�ɂ���
                        nextSlider.value = 0; // ���̃X���C�_�[�̒l�����Z�b�g����
                        SetSuccessPositionAndShowImage(); // ���̐����ʒu��ݒ肵�A�摜��\��
                    }
                    else
                    {
                        SceneManager.LoadScene("ResultScene"); // �ŏI���ʂ̃V�[�������[�h����
                    }
                }
                else
                {
                    SceneManager.LoadScene("ResultButScene"); // ���s���̃V�[�������[�h����
                }

                isClicked = false; // �N���b�N��Ԃ����Z�b�g����
            }
        }

        // ���݂̃X���C�_�[�̒l���X�V����
        if (currentSliderIndex < sliders.Count && !isClicked)
        {
            Slider activeSlider = sliders[currentSliderIndex];
            if (activeSlider.value >= 10)
            {
                maxValue = true;
            }
            else if (activeSlider.value <= 0)
            {
                maxValue = false;
            }

            activeSlider.value += maxValue ? -0.03f : 0.03f;
        }
    }

    // �����ʒu��ݒ肵�A�摜��\�����郁�\�b�h
    private void SetSuccessPositionAndShowImage()
    {
        if (currentSliderIndex < sliders.Count)
        {
            Slider currentSlider = sliders[currentSliderIndex];
            successPosition = UnityEngine.Random.Range(2, 8);
            minSuccessPosition = Mathf.Max(successPosition - 1, 2); // �ŏ��l��2�ȏ�ɂ���
            maxSuccessPosition = Mathf.Min(successPosition + 1, 8); // �ő�l��8�ȉ��ɂ���

            // �T�N�Z�X�|�W�V�����̈ʒu���f�o�b�O���O�ɏo�́i�����ň�x�����Ăяo���j
            UnityEngine.Debug.Log("Success Position: " + successPosition);

            // �摜�𐬌��ʒu�Ɉړ������ĕ\������
            RectTransform sliderRectTransform = currentSlider.GetComponent<RectTransform>();

            float minNormalizedPosition = minSuccessPosition / 5f;
            float maxNormalizedPosition = maxSuccessPosition / 5f;

            float minImageXPosition = Mathf.Lerp(sliderRectTransform.rect.xMin, sliderRectTransform.rect.xMax, minNormalizedPosition);
            float maxImageXPosition = Mathf.Lerp(sliderRectTransform.rect.xMin, sliderRectTransform.rect.xMax, maxNormalizedPosition);

            // �X���C�_�[�̃T�C�Y���擾
            float sliderWidth = sliderRectTransform.rect.width;
            float sliderHeight = sliderRectTransform.rect.height;

            // �����͈͉摜�̃T�C�Y���X���C�_�[�̃T�C�Y�ɍ��킹��
            successRangeImage.rectTransform.sizeDelta = new Vector2(maxImageXPosition - minImageXPosition, sliderHeight);

            // �����͈͉摜�̈ʒu��ݒ�
            successRangeImage.rectTransform.localPosition = new Vector3((minImageXPosition + maxImageXPosition) / 2 - (sliderWidth / 2), sliderRectTransform.rect.center.y, 0);

            successRangeImage.gameObject.SetActive(true);
        }
    }
}
