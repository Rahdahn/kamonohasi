using UnityEngine;
using UnityEngine.UI;

public class CircularGauge : MonoBehaviour
{
    public Image fillImage;  // 円形ゲージのフィルイメージ
    public float maxValue = 1f;  // ゲージの最大値
    public float minValue = 0f;  // ゲージの最小値
    private float currentValue = 0f;  // ゲージの現在値

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
