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

        // 最初の成功位置を設定し、画像を表示
        SetSuccessPositionAndShowImage();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = !isClicked;
            UnityEngine.Debug.Log(isClicked ? "stop" : "start");

            if (!isClicked) // ストップしたタイミングで判定
            {
                if (currentSliderIndex < sliders.Count)
                {
                    Slider currentSlider = sliders[currentSliderIndex];

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
                            SetSuccessPositionAndShowImage(); // 次の成功位置を設定し、画像を表示
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
            }
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

    private void SetSuccessPositionAndShowImage()
    {
        if (currentSliderIndex < sliders.Count)
        {
            Slider currentSlider = sliders[currentSliderIndex];
            successPosition = UnityEngine.Random.Range(2, 9);

            // サクセスポジションの位置をデバッグログに出力
            UnityEngine.Debug.Log("Success Position: " + successPosition);

            // 画像を成功位置に移動させて表示する
            RectTransform sliderRectTransform = currentSlider.GetComponent<RectTransform>();
            RectTransform fillRectTransform = currentSlider.fillRect;

            float normalizedPosition = successPosition / 10f;
            float imageXPosition = Mathf.Lerp(sliderRectTransform.rect.xMin, sliderRectTransform.rect.xMax, normalizedPosition);

            // スライダーのサイズを取得
            float sliderWidth = sliderRectTransform.rect.width;
            float sliderHeight = sliderRectTransform.rect.height;

            // MiddleImageのサイズをスライダーのサイズに合わせる
            middleImage.rectTransform.sizeDelta = new Vector2(sliderWidth * 0.1f, sliderHeight); // 幅をスライダーの幅の10%に設定

            // MiddleImageの位置を設定
            middleImage.rectTransform.localPosition = new Vector3(imageXPosition - (sliderWidth / 2), sliderRectTransform.rect.center.y, 0);

            middleImage.gameObject.SetActive(true);
        }
    }

    // 値を範囲にマップするヘルパー関数
    private float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) * (toMax - toMin) / (fromMax - fromMin) + toMin;
    }
}
