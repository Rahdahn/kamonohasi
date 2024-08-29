using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToGetButton : MonoBehaviour
{
    public Button getButton;

    void Start()
    {
        if (getButton != null)
        {
            getButton.onClick.AddListener(OnTitleButtonClick);
        }
    }

    void OnTitleButtonClick()
    {
        SceneManager.LoadScene("GetScene");
    }
}
