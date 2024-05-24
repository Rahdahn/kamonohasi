using UnityEngine;
using UnityEngine.UI;

public class RandomGauge : MonoBehaviour
{
    public Slider gaugeSlider; // Unity�Őݒ肵��Slider���֘A�t���邽�߂̕ϐ�

    public float minValue = 0f; // �Q�[�W�̍ŏ��l
    public float maxValue = 100f; // �Q�[�W�̍ő�l

    public float minChangeSpeed = 0.1f; // �Q�[�W���ω����鑬�x�̍ŏ��l
    public float maxChangeSpeed = 1f; // �Q�[�W���ω����鑬�x�̍ő�l

    private float currentValue; // ���݂̃Q�[�W�̒l
    private float targetValue; // �ڕW�̃Q�[�W�̒l
    private float changeSpeed; // �Q�[�W�̕ω����x

    void Start()
    {
        // �Q�[�W�̏����l��ݒ�
        currentValue = Random.Range(minValue, maxValue);
        gaugeSlider.value = currentValue;

        // �ڕW�̃Q�[�W�̒l�������_���ɐݒ�
        targetValue = Random.Range(minValue, maxValue);

        // �Q�[�W���ω����鑬�x�������_���ɐݒ�
        changeSpeed = Random.Range(minChangeSpeed, maxChangeSpeed);
    }

    void Update()
    {
        // �Q�[�W�̒l�����X�ɖڕW�̒l�Ɍ������ĕω�������
        currentValue = Mathf.MoveTowards(currentValue, targetValue, changeSpeed * Time.deltaTime);
        gaugeSlider.value = currentValue;

        // �Q�[�W���ڕW�̒l�ɒB������A�V�����ڕW�̒l�ƕω����x��ݒ肷��
        if (Mathf.Approximately(currentValue, targetValue))
        {
            targetValue = Random.Range(minValue, maxValue);
            changeSpeed = Random.Range(minChangeSpeed, maxChangeSpeed);
        }
    }
}
