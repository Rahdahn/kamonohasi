using UnityEngine;

public class ObjectAppearanceController : MonoBehaviour
{
    public GameObject objectToAppear; // 一時停止時に出現させるオブジェクト
    public GameObject additionalObjectToAppear; // もう一つの一時停止時に出現させるオブジェクト

    void Start()
    {
        // 最初にオブジェクトを非アクティブにする
        if (objectToAppear != null)
        {
            objectToAppear.SetActive(false);
        }
        if (additionalObjectToAppear != null)
        {
            additionalObjectToAppear.SetActive(false);
        }
    }

    public void ShowObjects()
    {
        if (objectToAppear != null)
        {
            objectToAppear.SetActive(true);
        }
        if (additionalObjectToAppear != null)
        {
            additionalObjectToAppear.SetActive(true);
        }
    }

    public void HideObjects()
    {
        if (objectToAppear != null)
        {
            objectToAppear.SetActive(false);
        }
        if (additionalObjectToAppear != null)
        {
            additionalObjectToAppear.SetActive(false);
        }
    }
}
