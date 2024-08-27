using UnityEngine;
using UnityEngine.UI;

public class GaugeController_Test : MonoBehaviour
{
    public Image gaugeImage;  // ゲージのImageコンポーネント
    public float fillSpeed = 0.5f;  // ゲージの充填速度
    public float maxFillAmount = 0.75f;  // ゲージの最大値（75%）

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
        // ゲージを充填させる
        if (currentFillAmount < maxFillAmount)
        {
            currentFillAmount += fillSpeed * Time.deltaTime;
            if (currentFillAmount > maxFillAmount)
            {
                currentFillAmount = maxFillAmount;
            }
            gaugeImage.fillAmount = currentFillAmount;
        }

        // ゲージが満タンになった場合の処理
        if (currentFillAmount >= maxFillAmount)
        {
            // ゲージをリセットする場合
            currentFillAmount = 0f;
            gaugeImage.fillAmount = currentFillAmount;
        }
    }

    public void SetFillAmount(float amount)
    {
        // スクリプト外からゲージの目標値を設定するメソッド
        currentFillAmount = Mathf.Clamp01(amount) * maxFillAmount;
        gaugeImage.fillAmount = currentFillAmount;
    }
}
