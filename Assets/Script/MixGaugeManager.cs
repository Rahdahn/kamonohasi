using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

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

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = !isClicked;
            UnityEngine.Debug.Log(isClicked ? "stop" : "start");
        }

        if (isClicked && currentSliderIndex < sliders.Count)
        {
            Slider currentSlider = sliders[currentSliderIndex];
            // �����_���ɐ����ʒu��ݒ�
            successPosition = UnityEngine.Random.Range(2, 9);

            UnityEngine.Debug.Log(successPosition);

            if (currentSlider.value >= successPosition - 1 && currentSlider.value <= successPosition +1)
            {
                currentSlider.gameObject.SetActive(false); // ���݂̃X���C�_�[���A�N�e�B�u�ɂ���
                currentSliderIndex++;
                if (currentSliderIndex < sliders.Count)
                {
                    Slider nextSlider = sliders[currentSliderIndex];
                    nextSlider.gameObject.SetActive(true); // ���̃X���C�_�[���A�N�e�B�u�ɂ���
                    nextSlider.value = 0; // ���̃X���C�_�[�̒l�����Z�b�g����
                    UnityEngine.Debug.Log("����");
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

    // �l��͈͂Ƀ}�b�v����w���p�[�֐�
    private float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) * (toMax - toMin) / (fromMax - fromMin) + toMin;
    }
}
