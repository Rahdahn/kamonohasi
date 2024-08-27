using UnityEngine;
using UnityEngine.UI;

public class GaugeController_Test : MonoBehaviour
{
    public Image gaugeImage;  // �Q�[�W��Image�R���|�[�l���g
    public float fillSpeed = 0.5f;  // �Q�[�W�̏[�U���x
    public float maxFillAmount = 0.75f;  // �Q�[�W�̍ő�l�i75%�j

    private float currentFillAmount = 0f;

    void Start()
    {
        if (gaugeImage == null)
        {
            Debug.LogError("Gauge Image is not assigned!");
        }
    }

    void Update()
    {
        // �Q�[�W���[�U������
        if (currentFillAmount < maxFillAmount)
        {
            currentFillAmount += fillSpeed * Time.deltaTime;
            if (currentFillAmount > maxFillAmount)
            {
                currentFillAmount = maxFillAmount;
            }
            gaugeImage.fillAmount = currentFillAmount;
        }

        // �Q�[�W�����^���ɂȂ����ꍇ�̏���
        if (currentFillAmount >= maxFillAmount)
        {
            // �Q�[�W�����Z�b�g����ꍇ
            currentFillAmount = 0f;
            gaugeImage.fillAmount = currentFillAmount;
        }
    }

    public void SetFillAmount(float amount)
    {
        // �X�N���v�g�O����Q�[�W�̖ڕW�l��ݒ肷�郁�\�b�h
        currentFillAmount = Mathf.Clamp01(amount) * maxFillAmount;
        gaugeImage.fillAmount = currentFillAmount;
    }
}
