using UnityEngine;
using UnityEngine.UI;

public class CircularGauge : MonoBehaviour
{
    public Image fillImage;  // �~�`�Q�[�W�̃t�B���C���[�W
    public float maxValue = 1f;  // �Q�[�W�̍ő�l
    public float minValue = 0f;  // �Q�[�W�̍ŏ��l
    private float currentValue = 0f;  // �Q�[�W�̌��ݒl

    public float CurrentValue
    {
        get { return currentValue; }
        set
        {
            currentValue = Mathf.Clamp(value, minValue, maxValue);
            UpdateFillImage();
        }
    }

    void Start()
    {
        UpdateFillImage();
    }

    void UpdateFillImage()
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = currentValue / maxValue;
        }
        else
        {
            Debug.LogError("Fill Image is not assigned!");
        }
    }

    public void IncreaseGauge(float amount)
    {
        CurrentValue += amount;
    }

    public void DecreaseGauge(float amount)
    {
        CurrentValue -= amount;
    }
}
