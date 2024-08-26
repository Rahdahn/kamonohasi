using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshProを使用するために必要

public class SceneSwitcher : MonoBehaviour
{
    public string sceneToLoad; // インスペクターでロードするシーンの名前を設定
    public TextMeshProUGUI countdownText; // インスペクターでCountdownText UIオブジェクトを設定
    public GaugeController gaugeController; // インスペクターでGaugeControllerの参照を設定

    private float timeRemaining = 60f; // 60秒（1分）

    void Update()
    {
        // スライダーが動いていないときにタイマーを進める
        if (gaugeController == null || !gaugeController.IsFilling())
        {
            // 残り時間を減らす
            timeRemaining -= Time.deltaTime;
        }

        // 残り時間を画面に表示
        countdownText.text = "残り時間: " + Mathf.Ceil(timeRemaining).ToString() + "秒";

        // 時間がなくなったらシーンを切り替える
        if (timeRemaining <= 0)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
