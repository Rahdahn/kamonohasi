using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public Text messageText;        // �\������e�L�X�g�R���|�[�l���g
    public float typingSpeed = 0.05f; // �ꕶ���\������Ԋu�i�b�j

    private string currentMessage = "";

    void Start()
    {
        messageText.text = ""; // ������Ԃł̓e�L�X�g����ɂ���
    }

    // �w�肳�ꂽ���b�Z�[�W���^�C�v���C�^�[���ʂŕ\������R���[�`��
    public IEnumerator ShowMessage(string message)
    {
        for (int i = 0; i <= message.Length; i++)
        {
            currentMessage = message.Substring(0, i);
            messageText.text = currentMessage;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // ���b�Z�[�W��A�����ĕ\�����邽�߂̃��\�b�h
    public void DisplayMessages(string[] messages)
    {
        StartCoroutine(DisplayMessagesCoroutine(messages));
    }

    private IEnumerator DisplayMessagesCoroutine(string[] messages)
    {
        foreach (string message in messages)
        {
            yield return StartCoroutine(ShowMessage(message));
            yield return new WaitForSeconds(1f); // ���b�Z�[�W�Ԃ̒x���i1�b�j
        }
    }
}
