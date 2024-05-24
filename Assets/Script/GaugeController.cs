using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    public Slider gaugeSlider; // ゲージ用スライダー
    public Transform targetObject; // 動かしたいオブジェクト
    public Slider meterSlider; // メーター用スライダー

    private float previousGaugeValue; // 前回のゲージの値

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
            // ゲージの値に応じてオブジェクトを動かす
            float currentValue = gaugeSlider.value;
            float movement = currentValue - previousGaugeValue;
            targetObject.Translate(Vector3.right * movement); // 右方向に移動

            // 条件を満たした場合にメーターを増やす
            if (Mathf.Abs(movement) > 0.1f) // 例えば、一定の動きがあった場合
            {
                IncreaseMeter(0.01f); // メーターを増加させる
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
