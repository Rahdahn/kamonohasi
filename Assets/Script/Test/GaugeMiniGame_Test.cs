using UnityEngine;

public class GaugeMiniGame_Test : MonoBehaviour
{
    public GaugeController_Test gaugeController;
    public float successThreshold = 0.1f;  // 成功範囲の閾値（0.1は10%の範囲）

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckSuccess();
        }
    }

    void CheckSuccess()
    {
        if (gaugeController == null || gaugeController.gaugeImage == null)
        {
            Debug.LogError("GaugeController or Gauge Image is not assigned!");
            return;
        }

        // 現在のゲージの値
        float fillAmount = gaugeController.gaugeImage.fillAmount;

        float successRangeMin = 0.75f - successThreshold;
        float successRangeMax = 0.75f + successThreshold;

        // ゲージが成功範囲内かどうかをチェック
        if (fillAmount >= successRangeMin && fillAmount <= successRangeMax)
        {
            Debug.Log("成功!");
        }
        else
        {
            Debug.Log("失敗!");
        }
    }
}
