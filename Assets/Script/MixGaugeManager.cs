using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MixGaugeManager : MonoBehaviour
{
    [SerializeField] List<Slider> sliders; // スライダーのリスト
    [SerializeField] UnityEngine.UI.Image successRangeImage; // 成功範囲を示す画像

    private int currentSliderIndex = 0; // 現在のスライダーのインデックス
    private bool isClicked = false; // クリック状態を保持するフラグ
    private bool maxValue = false; // スライダーの値が最大かどうかを示すフラグ
    private int successPosition; // 成功位置
    private int minSuccessPosition; // 成功位置の最小値
    private int maxSuccessPosition; // 成功位置の最大値

    void Start()
    {
        Initialize();
    }

    // 初期化メソッド
    public void Initialize()
    {
        // 全てのスライダーを初期状態に設定
        foreach (Slider slider in sliders)
        {
            slider.value = 0;
            slider.gameObject.SetActive(false);
        }

        // 最初のスライダーをアクティブにする
        if (sliders.Count > 0)
        {
            sliders[0].gameObject.SetActive(true);
        }

        maxValue = false;
        isClicked = false;
        currentSliderIndex = 0;
        successRangeImage.gameObject.SetActive(false);

        // 最初の成功位置を設定し、画像を表示
        if (!successRangeImage.gameObject.activeSelf) // 成功範囲画像が非アクティブの場合のみ
        {
            SetSuccessPositionAndShowImage();
        }
    }

    void Update()
    {
        // マウスボタンが押されたときの処理
        if (Input.GetMouseButtonDown(0) && !isClicked)
        {
            isClicked = true;

            // ストップしたタイミングで判定
            if (currentSliderIndex < sliders.Count)
            {
                Slider currentSlider = sliders[currentSliderIndex];

                // 成功範囲内かどうかを判定
                if (currentSlider.value >= minSuccessPosition && currentSlider.value <= maxSuccessPosition)
                {
                    currentSlider.gameObject.SetActive(false); // 現在のスライダーを非アクティブにする
                    successRangeImage.gameObject.SetActive(false); // 成功範囲の画像を非表示にする
                    currentSliderIndex++;

                    if (currentSliderIndex < sliders.Count)
                    {
                        Slider nextSlider = sliders[currentSliderIndex];
                        nextSlider.gameObject.SetActive(true); // 次のスライダーをアクティブにする
                        nextSlider.value = 0; // 次のスライダーの値をリセットする
                        SetSuccessPositionAndShowImage(); // 次の成功位置を設定し、画像を表示
                    }
                    else
                    {
                        SceneManager.LoadScene("ResultScene"); // 最終結果のシーンをロードする
                    }
                }
                else
                {
                    SceneManager.LoadScene("ResultButScene"); // 失敗時のシーンをロードする
                }

                isClicked = false; // クリック状態をリセットする
            }
        }

        // 現在のスライダーの値を更新する
        if (currentSliderIndex < sliders.Count && !isClicked)
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

    // 成功位置を設定し、画像を表示するメソッド
    private void SetSuccessPositionAndShowImage()
    {
        if (currentSliderIndex < sliders.Count)
        {
            Slider currentSlider = sliders[currentSliderIndex];
            successPosition = UnityEngine.Random.Range(2, 8);
            minSuccessPosition = Mathf.Max(successPosition - 1, 2); // 最小値を2以上にする
            maxSuccessPosition = Mathf.Min(successPosition + 1, 8); // 最大値を8以下にする

            // サクセスポジションの位置をデバッグログに出力（ここで一度だけ呼び出す）
            UnityEngine.Debug.Log("Success Position: " + successPosition);

            // 画像を成功位置に移動させて表示する
            RectTransform sliderRectTransform = currentSlider.GetComponent<RectTransform>();

            float minNormalizedPosition = minSuccessPosition / 5f;
            float maxNormalizedPosition = maxSuccessPosition / 5f;

            float minImageXPosition = Mathf.Lerp(sliderRectTransform.rect.xMin, sliderRectTransform.rect.xMax, minNormalizedPosition);
            float maxImageXPosition = Mathf.Lerp(sliderRectTransform.rect.xMin, sliderRectTransform.rect.xMax, maxNormalizedPosition);

            // スライダーのサイズを取得
            float sliderWidth = sliderRectTransform.rect.width;
            float sliderHeight = sliderRectTransform.rect.height;

            // 成功範囲画像のサイズをスライダーのサイズに合わせる
            successRangeImage.rectTransform.sizeDelta = new Vector2(maxImageXPosition - minImageXPosition, sliderHeight);

            // 成功範囲画像の位置を設定
            successRangeImage.rectTransform.localPosition = new Vector3((minImageXPosition + maxImageXPosition) / 2 - (sliderWidth / 2), sliderRectTransform.rect.center.y, 0);

            successRangeImage.gameObject.SetActive(true);
        }
    }
}
