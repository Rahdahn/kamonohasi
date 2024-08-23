using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel; // チュートリアルメッセージを表示するパネル
    public Button closeButton;       // チュートリアルを閉じるボタン
    public Button resetButton;       // チュートリアル表示回数をリセットするボタン
    private const string TutorialSeenKey = "TutorialSeen"; // PlayerPrefsキー

    void Start()
    {
        // チュートリアルが初回かどうかをチェック
        if (PlayerPrefs.GetInt(TutorialSeenKey, 0) == 0)
        {
            ShowTutorial();
        }
        else
        {
            HideTutorial();
        }

        // ボタンのクリックイベントを設定
        closeButton.onClick.AddListener(CloseTutorial);
        resetButton.onClick.AddListener(ResetTutorial);
    }

    void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    void HideTutorial()
    {
        tutorialPanel.SetActive(false);
    }

    void CloseTutorial()
    {
        HideTutorial();
        PlayerPrefs.SetInt(TutorialSeenKey, 1);
        PlayerPrefs.Save();
    }

    void ResetTutorial()
    {
        PlayerPrefs.SetInt(TutorialSeenKey, 0);
        PlayerPrefs.Save();
    }
}
