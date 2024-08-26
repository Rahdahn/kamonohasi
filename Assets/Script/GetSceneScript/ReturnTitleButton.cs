using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnTitleButton : MonoBehaviour
{
    public Button titleButton;

    void Start()
    {
        if (titleButton != null)
        {
            titleButton.onClick.AddListener(OnTitleButtonClick);
        }
    }

    void OnTitleButtonClick()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
