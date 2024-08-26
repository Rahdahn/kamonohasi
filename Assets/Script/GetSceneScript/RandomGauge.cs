using UnityEngine;
using UnityEngine.UI;

public class RandomGauge : MonoBehaviour
{
    public Slider gaugeSlider; // Unityで設定したSliderを関連付けるための変数

    public float minValue = 0f; // ゲージの最小値
    public float maxValue = 100f; // ゲージの最大値

    public float minChangeSpeed = 0.1f; // ゲージが変化する速度の最小値
    public float maxChangeSpeed = 1f; // ゲージが変化する速度の最大値

    private float currentValue; // 現在のゲージの値
    private float targetValue; // 目標のゲージの値
    private float changeSpeed; // ゲージの変化速度

    void Start()
    {
        // ゲージの初期値を設定
        currentValue = Random.Range(minValue, maxValue);
        gaugeSlider.value = currentValue;

        // 目標のゲージの値をランダムに設定
        targetValue = Random.Range(minValue, maxValue);

        // ゲージが変化する速度をランダムに設定
        changeSpeed = Random.Range(minChangeSpeed, maxChangeSpeed);
    }

    void Update()
    {
        // ゲージの値を徐々に目標の値に向かって変化させる
        currentValue = Mathf.MoveTowards(currentValue, targetValue, changeSpeed * Time.deltaTime);
        gaugeSlider.value = currentValue;

        // ゲージが目標の値に達したら、新しい目標の値と変化速度を設定する
        if (Mathf.Approximately(currentValue, targetValue))
        {
            targetValue = Random.Range(minValue, maxValue);
            changeSpeed = Random.Range(minChangeSpeed, maxChangeSpeed);
        }
    }
}
