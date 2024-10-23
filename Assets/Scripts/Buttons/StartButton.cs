using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Button titleButton;

    void Start()
    {
        if (titleButton != null)
        {
            titleButton.onClick.AddListener(OnStartButtonClick);
        }
    }

    void OnStartButtonClick()
    {
        // �Q�[���f�[�^�̃��Z�b�g
        GameData.ResetCounts();

        // �V�[����J��
        FadeManager.Instance.LoadScene("GetScene", 1.0f);
    }
}
