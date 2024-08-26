using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;
    public Button startButton;  // 開始ボタンを参照するための変数
    public GameObject[] uiElementsToHide;  // 非表示にするUI要素の配列
    public GameObject displayObject; // 表示するオブジェクト

    private void Start()
    {
        // ボタンのクリックイベントにリスナーを追加
        startButton.onClick.AddListener(StartTimerOnClick);
        DisplayTime(timeRemaining);  // 初期時間を表示
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;

                // 指定の時間が経過したら画面を表示するコルーチンを開始
                StartCoroutine(DisplayTimeout());
            }
        }
    }

    // ボタンクリックでタイマーを開始するメソッド
    private void StartTimerOnClick()
    {
        if (!timerIsRunning) // タイマーが既に動作中でない場合に開始
        {
            timerIsRunning = true;
            Debug.Log("Timer started by button click.");
            HideUIElements();
        }
    }

    private IEnumerator DisplayTimeout()
    {
        yield return new WaitForSeconds(0f); // 2秒待機

        // 画面を表示するオブジェクトをアクティブにする
        displayObject.SetActive(true);
    }

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

      

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void HideUIElements()
    {
        foreach (GameObject uiElement in uiElementsToHide)
        {
            uiElement.SetActive(false);  // UI要素を非表示にする
        }
    }
}
