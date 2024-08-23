using UnityEngine;

public class GaugeControlManager : MonoBehaviour
{
    public GaugeController gaugeController;  // GaugeController�̎Q��

    void Start()
    {
        // �K�v�ɉ����āAGaugeController�̓���𐧌�
        if (gaugeController != null)
        {
            gaugeController.ActivateSliderAndButton();  // �X���C�_�[�ƃ{�^�����A�N�e�B�u�ɂ���
        }
    }

    void Update()
    {
        // �X�y�[�X�L�[�������ƃX���C�_�[���J�n�܂��͒�~
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gaugeController != null)
            {
                if (gaugeController != null)
                {
                    gaugeController.StartFilling();  // �X���C�_�[�̓������J�n
                }
            }
        }

        // ���̃{�^���ȂǂŃX���C�_�[���~����ꍇ
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (gaugeController != null)
            {
                gaugeController.StopFilling();  // �X���C�_�[�̓������~
            }
        }
    }
}
