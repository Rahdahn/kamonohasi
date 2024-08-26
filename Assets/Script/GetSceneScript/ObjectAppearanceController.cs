using UnityEngine;

public class ObjectAppearanceController : MonoBehaviour
{
    public GameObject objectToAppear; // �ꎞ��~���ɏo��������I�u�W�F�N�g
    public GameObject additionalObjectToAppear; // ������̈ꎞ��~���ɏo��������I�u�W�F�N�g

    void Start()
    {
        // �ŏ��ɃI�u�W�F�N�g���A�N�e�B�u�ɂ���
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
