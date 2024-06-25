using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MixGaugeManager : MonoBehaviour
{
    [SerializeField] List<Slider> sliders;
    [SerializeField] UnityEngine.UI.Image middleImage; // �V���A���C�Y�ŃA�^�b�`���ꂽ�摜
    private int currentSliderIndex = 0;
    private bool isClicked;
    private bool maxValue;
    private int successPosition;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        foreach (Slider slider in sliders)
        {
            slider.value = 0;
            slider.gameObject.SetActive(false); // �S�ẴX���C�_�[��������ԂŔ�A�N�e�B�u�ɂ���
        }
        if (sliders.Count > 0)
        {
            sliders[0].gameObject.SetActive(true); // �ŏ��̃X���C�_�[���A�N�e�B�u�ɂ���
        }
        maxValue = false;
        isClicked = false;
        currentSliderIndex = 0; // �C���f�b�N�X������������
        middleImage.gameObject.SetActive(false); // �摜���\���ɂ���

        // �ŏ��̐����ʒu��ݒ肵�A�摜��\��
        SetSuccessPositionAndShowImage();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = !isClicked;
            UnityEngine.Debug.Log(isClicked ? "stop" : "start");

            if (!isClicked) // �X�g�b�v�����^�C�~���O�Ŕ���
            {
                if (currentSliderIndex < sliders.Count)
                {
                    Slider currentSlider = sliders[currentSliderIndex];

                    if (currentSlider.value >= successPosition - 1 && currentSlider.value <= successPosition + 1)
                    {
                        currentSlider.gameObject.SetActive(false); // ���݂̃X���C�_�[���A�N�e�B�u�ɂ���
                        middleImage.gameObject.SetActive(false); // �摜���\���ɂ���
                        currentSliderIndex++;
                        if (currentSliderIndex < sliders.Count)
                        {
                            Slider nextSlider = sliders[currentSliderIndex];
                            nextSlider.gameObject.SetActive(true); // ���̃X���C�_�[���A�N�e�B�u�ɂ���
                            nextSlider.value = 0; // ���̃X���C�_�[�̒l�����Z�b�g����
                            UnityEngine.Debug.Log("����");
                            SetSuccessPositionAndShowImage(); // ���̐����ʒu��ݒ肵�A�摜��\��
                        }
                        else
                        {
                            UnityEngine.Debug.Log("Great!!");
                            SceneManager.LoadScene("ResultScene"); // �ŏI���ʂ̃V�[�������[�h����
                        }
                    }
                    else
                    {
                        UnityEngine.Debug.Log("���s");
                        SceneManager.LoadScene("ResultButScene"); // ���s���̃V�[�������[�h����
                    }
                    isClicked = false; // �N���b�N��Ԃ����Z�b�g����
                    return;
                }
            }
        }

        if (currentSliderIndex < sliders.Count)
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

    private void SetSuccessPositionAndShowImage()
    {
        if (currentSliderIndex < sliders.Count)
        {
            Slider currentSlider = sliders[currentSliderIndex];
            successPosition = UnityEngine.Random.Range(2, 9);

            // �T�N�Z�X�|�W�V�����̈ʒu���f�o�b�O���O�ɏo��
            UnityEngine.Debug.Log("Success Position: " + successPosition);

            // �摜�𐬌��ʒu�Ɉړ������ĕ\������
            RectTransform sliderRectTransform = currentSlider.GetComponent<RectTransform>();
            RectTransform fillRectTransform = currentSlider.fillRect;

            float normalizedPosition = successPosition / 10f;
            float imageXPosition = Mathf.Lerp(sliderRectTransform.rect.xMin, sliderRectTransform.rect.xMax, normalizedPosition);

            // �X���C�_�[�̃T�C�Y���擾
            float sliderWidth = sliderRectTransform.rect.width;
            float sliderHeight = sliderRectTransform.rect.height;

            // MiddleImage�̃T�C�Y���X���C�_�[�̃T�C�Y�ɍ��킹��
            middleImage.rectTransform.sizeDelta = new Vector2(sliderWidth * 0.1f, sliderHeight); // �����X���C�_�[�̕���10%�ɐݒ�

            // MiddleImage�̈ʒu��ݒ�
            middleImage.rectTransform.localPosition = new Vector3(imageXPosition - (sliderWidth / 2), sliderRectTransform.rect.center.y, 0);

            middleImage.gameObject.SetActive(true);
        }
    }

    // �l��͈͂Ƀ}�b�v����w���p�[�֐�
    private float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) * (toMax - toMin) / (fromMax - fromMin) + toMin;
    }
}
