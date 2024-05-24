using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SliderMove2 : MonoBehaviour
{
    [SerializeField] Slider currentSlider;
    [SerializeField] Slider nextSlider;
    private bool isClicked;
    private bool maxValue;

    void Start()
    {
        currentSlider.value = 0;
        nextSlider.gameObject.SetActive(false); // 次のスライダーを非アクティブにする
        maxValue = false;
        isClicked = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isClicked == false)
        {
            UnityEngine.Debug.Log("stop");
            isClicked = true;
        }
        else if (Input.GetMouseButtonDown(0) && isClicked == true)
        {
            UnityEngine.Debug.Log("start");
            isClicked = false;
        }
        if (isClicked)
        {
            if (currentSlider.value >= 4.5 && currentSlider.value <= 5.5)
            {
                currentSlider.gameObject.SetActive(false); // 現在のスライダーを非アクティブにする
                nextSlider.gameObject.SetActive(true); // 次のスライダーをアクティブにする
                nextSlider.value = 0; // 次のスライダーの値を初期化する
                UnityEngine.Debug.Log("成功");
            }
            else
            {
                // クリックされたが条件に合致しない場合の処理を追加する
                UnityEngine.Debug.Log("失敗");
                SceneManager.LoadScene("ResultButScene");
            }
            return;
        }

        if (currentSlider.value == 10)
        {
            maxValue = true;
        }
        if (currentSlider.value == 0)
        {
            maxValue = false;
        }

        if (maxValue == true)
        {
            currentSlider.value -= 0.03f;
        }

        if (maxValue == false)
        {
            currentSlider.value += 0.03f;
        }
    }
}
