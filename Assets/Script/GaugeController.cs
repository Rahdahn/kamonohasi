using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public Slider gaugeSlider; // ゲージ用スライダー
    public float increaseAmount = 0.01f; // ゲージの増加量

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ゲージ用のオブジェクトと重なった場合にゲージを増加させる
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
