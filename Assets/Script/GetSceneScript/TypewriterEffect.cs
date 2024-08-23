using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public Text messageText;        // 表示するテキストコンポーネント
    public float typingSpeed = 0.05f; // 一文字表示する間隔（秒）

    private string currentMessage = "";

    void Start()
    {
        messageText.text = ""; // 初期状態ではテキストを空にする
    }

    // 指定されたメッセージをタイプライター効果で表示するコルーチン
    public IEnumerator ShowMessage(string message)
    {
        for (int i = 0; i <= message.Length; i++)
        {
            currentMessage = message.Substring(0, i);
            messageText.text = currentMessage;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // メッセージを連続して表示するためのメソッド
    public void DisplayMessages(string[] messages)
    {
        StartCoroutine(DisplayMessagesCoroutine(messages));
    }

    private IEnumerator DisplayMessagesCoroutine(string[] messages)
    {
        foreach (string message in messages)
        {
            yield return StartCoroutine(ShowMessage(message));
            yield return new WaitForSeconds(1f); // メッセージ間の遅延（1秒）
        }
    }
}
