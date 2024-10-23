using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public Image gaugeImage;
    public float fillSpeed = 0.5f;  // �Q�[�W�̏[�U���x
    public float maxFillAmount = 0.75f;  // �Q�[�W�̍ő�l�i75%�j

    private float currentFillAmount = 0f;
    private bool isFilling = true;


    void Update()
    {
        // �Q�[�W���[�U�܂��͌��������鏈��
        if (isFilling)
        {
            currentFillAmount += fillSpeed * Time.deltaTime;
            if (currentFillAmount >= maxFillAmount)
            {
                currentFillAmount = maxFillAmount;
                isFilling = false;  // �[�U�����������猸���t�F�[�Y�Ɉڍs
            }
        }
        else
        {
            currentFillAmount -= fillSpeed * Time.deltaTime;
            if (currentFillAmount <= 0f)
            {
                currentFillAmount = 0f;
                isFilling = true;  // ����������������[�U�t�F�[�Y�ɖ߂�
            }
        }

        gaugeImage.fillAmount = currentFillAmount;
    }

    public void SetFillAmount(float amount)
    {
        currentFillAmount = Mathf.Clamp01(amount) * maxFillAmount;
        gaugeImage.fillAmount = currentFillAmount;
    }

    public void ResetGauge()
    {
        currentFillAmount = 0f;
        gaugeImage.fillAmount = currentFillAmount;
        isFilling = true; // �[�U�t�F�[�Y�ɖ߂�
    }
}