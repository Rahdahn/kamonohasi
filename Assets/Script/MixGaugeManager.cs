using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MixGaugeManager : MonoBehaviour
{
    [SerializeField] List<Slider> sliders;
    [SerializeField] UnityEngine.UI.Image middleImage; // シリアライズでアタッチされた画像
    private int currentSliderIndex = 0;
    private bool isClicked;
    private bool maxValue;
    private int successPosition;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        foreach (Slider slider in sliders)
        {
            slider.value = 0;
            slider.gameObject.SetActive(false); // 全てのスライダーを初期状態で非アクティブにする
        }
        if (sliders.Count > 0)
        {
            sliders[0].gameObject.SetActive(true); // 最初のスライダーをアクティブにする
        }
        maxValue = false;
        isClicked = false;
        currentSliderIndex = 0; // インデックスを初期化する
        middleImage.gameObject.SetActive(false); // 画像を非表示にする
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = !isClicked;
            UnityEngine.Debug.Log(isClicked ? "stop" : "start");
        }

        if (isClicked && currentSliderIndex < sliders.Count)
        {
            Slider currentSlider = sliders[currentSliderIndex];
            // ランダムに成功位置を設定
            successPosition = UnityEngine.Random.Range(2, 9);

            // サクセスポジションの位置をデバッグログに出力
            UnityEngine.Debug.Log("Success Position: " + successPosition);

            // 画像を成功位置に移動させて表示する
            Vector3 imagePosition = currentSlider.fillRect.position;
            imagePosition.x = Map(successPosition, 0, 10, currentSlider.fillRect.rect.xMin, currentSlider.fillRect.rect.xMax);
            middleImage.rectTransform.position = imagePosition;
            middleImage.gameObject.SetActive(true);

            if (currentSlider.value >= successPosition - 1 && currentSlider.value <= successPosition + 1)
            {
                currentSlider.gameObject.SetActive(false); // 現在のスライダーを非アクティブにする
                middleImage.gameObject.SetActive(false); // 画像を非表示にする
                currentSliderIndex++;
                if (currentSliderIndex < sliders.Count)
                {
                    Slider nextSlider = sliders[currentSliderIndex];
                    nextSlider.gameObject.SetActive(true); // 次のスライダーをアクティブにする
                    nextSlider.value = 0; // 次のスライダーの値をリセットする
                    UnityEngine.Debug.Log("成功");
                }
                else
                {
                    UnityEngine.Debug.Log("Great!!");
                    SceneManager.LoadScene("ResultScene"); // 最終結果のシーンをロードする
                }
            }
            else
            {
                UnityEngine.Debug.Log("失敗");
                SceneManager.LoadScene("ResultButScene"); // 失敗時のシーンをロードする
            }
            isClicked = false; // クリック状態をリセットする
            return;
        }

        if (currentSliderIndex < sliders.Count)
        {
            Slider activeSlider = sliders[currentSliderIndex];
            if (activeSlider.value >= 10)
            {
                maxValue = true;
            }
            else if (activeSlider.value <= 0)
            {
                maxValue = false;
            }

            activeSlider.value += maxValue ? -0.03f : 0.03f;
        }
    }

    // 値を範囲にマップするヘルパー関数
    private float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) * (toMax - toMin) / (fromMax - fromMin) + toMin;
    }
}
