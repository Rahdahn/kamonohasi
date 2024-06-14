using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public Slider gaugeSlider; // �Q�[�W�p�X���C�_�[
    public float increaseAmount = 0.01f; // �Q�[�W�̑�����

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �Q�[�W�p�̃I�u�W�F�N�g�Əd�Ȃ����ꍇ�ɃQ�[�W�𑝉�������
        if (other.CompareTag("GaugeObject"))
        {
            IncreaseGauge();
        }
    }

    private void IncreaseGauge()
    {
        if (gaugeSlider != null)
        {
            gaugeSlider.value += increaseAmount;
        }
    }
}
