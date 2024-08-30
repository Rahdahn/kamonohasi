using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToMixButton : MonoBehaviour
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
        FadeManager.Instance.LoadScene("MixScene(Test)", 1.0f);
    }
}