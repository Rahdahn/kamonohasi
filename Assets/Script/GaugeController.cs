using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public Slider gaugeSlider; // �Q�[�W�p�X���C�_�[
    public Transform targetObject; // �����������I�u�W�F�N�g
    public Slider meterSlider; // ���[�^�[�p�X���C�_�[

    private float previousGaugeValue; // �O��̃Q�[�W�̒l

    void Start()
    {
        if (gaugeSlider != null)
        {
            previousGaugeValue = gaugeSlider.value;
        }
    }

    void Update()
    {
        if (gaugeSlider != null && targetObject != null)
        {
            // �Q�[�W�̒l�ɉ����ăI�u�W�F�N�g�𓮂���
            float currentValue = gaugeSlider.value;
            float movement = currentValue - previousGaugeValue;
            targetObject.Translate(Vector3.right * movement); // �E�����Ɉړ�

            // �����𖞂������ꍇ�Ƀ��[�^�[�𑝂₷
            if (Mathf.Abs(movement) > 0.1f) // �Ⴆ�΁A���̓������������ꍇ
            {
                IncreaseMeter(0.01f); // ���[�^�[�𑝉�������
            }

            previousGaugeValue = currentValue;
        }
    }

    void IncreaseMeter(float amount)
    {
        if (meterSlider != null)
        {
            meterSlider.value += amount;
        }
    }
}
