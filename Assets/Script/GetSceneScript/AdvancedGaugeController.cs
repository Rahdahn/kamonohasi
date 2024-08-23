using UnityEngine;
using UnityEngine.UI;

public class AdvancedGaugeController : MonoBehaviour
{
    [System.Serializable]
    public class Gauge
    {
        public Slider gaugeSlider;
        public Transform targetObject;
        public Image gaugeFill; // ゲージのバーのImageコンポーネントを参照
        public Color minColor; // 最小値の色
        public Color maxColor; // 最大値の色
        public float minValue;
        public float maxValue;
        public float moveSpeed;
        public float requiredTime;
    }

    public Gauge[] gauges;
    public Slider meterSlider;
    public Image meterFill; // メーターのバーのImageコンポーネントを参照
    public Color meterMinColor; // メーターのバーの最小値の色
    public Color meterMaxColor; // メーターのバーの最大値の色
    public float meterIncreaseAmount = 0.01f;

    private float[] stayTimes;

    void Start()
    {
        stayTimes = new float[gauges.Length];
    }

    void Update()
    {
        for (int i = 0; i < gauges.Length; i++)
        {
            var gauge = gauges[i];
            if (gauge.gaugeSlider != null && gauge.targetObject != null)
            {
                float currentValue = gauge.gaugeSlider.value;
                float targetPosition = Mathf.Lerp(gauge.minValue, gauge.maxValue, currentValue);

                // オブジェクトの位置を更新
                gauge.targetObject.localPosition = new Vector3(targetPosition, gauge.targetObject.localPosition.y, gauge.targetObject.localPosition.z);

                // ゲージのバーの色を更新
                if (gauge.gaugeFill != null)
                {
                    float fillAmount = Mathf.InverseLerp(gauge.minValue, gauge.maxValue, currentValue);
                    gauge.gaugeFill.color = Color.Lerp(gauge.minColor, gauge.maxColor, fillAmount);
                }

                // ゲージが特定の範囲内にあるかをチェック
                if (currentValue >= gauge.minValue && currentValue <= gauge.maxValue)
                {
                    stayTimes[i] += Time.deltaTime; // 範囲内にいる時間を加算
                    if (stayTimes[i] >= gauge.requiredTime)
                    {
                        IncreaseMeter(meterIncreaseAmount); // メーターを増加させる
                        stayTimes[i] = 0; // 時間をリセット
                    }
                }
                else
                {
                    stayTimes[i] = 0; // 範囲外に出た場合は時間をリセット
                }
            }
        }

        // メーターのバーの色を更新
        if (meterFill != null && meterSlider != null)
        {
            float fillAmount = Mathf.InverseLerp(meterSlider.minValue, meterSlider.maxValue, meterSlider.value);
            meterFill.color = Color.Lerp(meterMinColor, meterMaxColor, fillAmount);
        }

        // 例: キー入力に応じて範囲を移動させる
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveGaugeRange(0, 0.1f); // 例: ゲージ0の範囲を上に移動
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveGaugeRange(0, -0.1f); // 例: ゲージ0の範囲を下に移動
        }
    }

    void IncreaseMeter(float amount)
    {
        if (meterSlider != null)
        {
            meterSlider.value += amount; // メーターを増加させる
        }
    }

    // 範囲を移動させるメソッド
    void MoveGaugeRange(int gaugeIndex, float delta)
    {
        if (gaugeIndex >= 0 && gaugeIndex < gauges.Length)
        {
            Gauge gauge = gauges[gaugeIndex];
            gauge.minValue += delta;
            gauge.maxValue += delta;

            // 範囲が0〜1を超えないように調整
            gauge.minValue = Mathf.Clamp(gauge.minValue, 0, 1);
            gauge.maxValue = Mathf.Clamp(gauge.maxValue, 0, 1);

            // minValueがmaxValueを超えないようにする
            if (gauge.minValue > gauge.maxValue)
            {
                gauge.minValue = gauge.maxValue;
            }
        }
    }
}
