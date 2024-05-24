using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel; // �`���[�g���A�����b�Z�[�W��\������p�l��
    public Button closeButton;       // �`���[�g���A�������{�^��
    public Button resetButton;       // �`���[�g���A���\���񐔂����Z�b�g����{�^��
    private const string TutorialSeenKey = "TutorialSeen"; // PlayerPrefs�L�[

    void Start()
    {
        // �`���[�g���A�������񂩂ǂ������`�F�b�N
        if (PlayerPrefs.GetInt(TutorialSeenKey, 0) == 0)
        {
            ShowTutorial();
        }
        else
        {
            HideTutorial();
        }

        // �{�^���̃N���b�N�C�x���g��ݒ�
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
