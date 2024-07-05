using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Diagnostics;

public class SliderGameController : MonoBehaviour
{
    public Slider slider;
    public UnityEngine.UI.Image successImage;

    private int currentSliderIndex = 1;
    private float successRangeMin;
    private float successRangeMax;

    void Start()
    {
        // 初期の成功範囲を設定
        SetSuccessRange();

        // スライダー移動のコルーチンを開始
        StartCoroutine(MoveSlider());
    }

    void SetSuccessRange()
    {
        // 成功範囲をランダムに決定
        int randomValue = UnityEngine.Random.Range(2, 10); // 2から9の間でランダム値を取得
        successRangeMin = randomValue - 1;
        successRangeMax = randomValue + 1;

        // 成功範囲をデバッグログに表示
        UnityEngine.Debug.Log("Success Range: " + successRangeMin + " to " + successRangeMax);

        // 成功範囲にsuccessImageを配置
        float normalizedMin = (successRangeMin - slider.minValue) / (slider.maxValue - slider.minValue);
        float normalizedMax = (successRangeMax - slider.minValue) / (slider.maxValue - slider.minValue);
        float minPosition = slider.fillRect.position.x + normalizedMin * slider.fillRect.rect.width;
        float maxPosition = slider.fillRect.position.x + normalizedMax * slider.fillRect.rect.width;

        successImage.rectTransform.position = new Vector3((minPosition + maxPosition) / 2, successImage.rectTransform.position.y, successImage.rectTransform.position.z);
        successImage.rectTransform.sizeDelta = new Vector2(maxPosition - minPosition, successImage.rectTransform.sizeDelta.y);
        successImage.gameObject.SetActive(true); // 成功範囲画像を表示
    }

    IEnumerator MoveSlider()
    {
        float sliderValue = 0f;
        float direction = 1f; // 1は右に移動、-1は左に移動を意味する

        while (true)
        {
            // スライダーの値を更新
            sliderValue += Time.deltaTime * 2f * direction; // 速度を調整
            slider.value = sliderValue;

            // スライダーが端に達したら方向を変更
            if (sliderValue >= slider.maxValue || sliderValue <= slider.minValue)
            {
                direction *= -1f; // 方向を変更
            }

            // 成功範囲内のクリックをチェック
            if (Input.GetMouseButtonDown(0))
            {
                float clickPosition = Input.mousePosition.x;

                if (clickPosition >= slider.fillRect.position.x && clickPosition <= slider.fillRect.position.x + slider.fillRect.rect.width)
                {
                    float normalizedClick = (clickPosition - slider.fillRect.position.x) / slider.fillRect.rect.width;
                    float actualValue = Mathf.Lerp(slider.minValue, slider.maxValue, normalizedClick);

                    if (actualValue >= successRangeMin && actualValue <= successRangeMax)
                    {
                        // 成功したクリック、次のスライダーまたは結果シーンへ移動
                        if (currentSliderIndex < 4)
                        {
                            currentSliderIndex++;
                            SetSuccessRange(); // 次のスライダーのために新しい成功範囲を設定
                        }
                        else
                        {
                            SceneManager.LoadScene("ResultScene"); // 結果シーンをロード
                        }
                    }
                    else
                    {
                        SceneManager.LoadScene("ResultButScene"); // 結果でも失敗シーンをロード
                    }
                }
            }

            yield return null;
        }
    }
}
