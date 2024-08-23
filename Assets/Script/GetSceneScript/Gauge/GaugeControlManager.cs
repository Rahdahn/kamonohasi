using UnityEngine;

public class GaugeControlManager : MonoBehaviour
{
    public GaugeController gaugeController;  // GaugeControllerの参照

    void Start()
    {
        // 必要に応じて、GaugeControllerの動作を制御
        if (gaugeController != null)
        {
            gaugeController.ActivateSliderAndButton();  // スライダーとボタンをアクティブにする
        }
    }

    void Update()
    {
        // スペースキーを押すとスライダーを開始または停止
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gaugeController != null)
            {
                if (gaugeController != null)
                {
                    gaugeController.StartFilling();  // スライダーの動きを開始
                }
            }
        }

        // 他のボタンなどでスライダーを停止する場合
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (gaugeController != null)
            {
                gaugeController.StopFilling();  // スライダーの動きを停止
            }
        }
    }
}
