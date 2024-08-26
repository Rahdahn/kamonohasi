using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro���g�p
using UnityEngine.UI;

public class CollectSceneManager : MonoBehaviour
{
    public Transform imageContainer; // ������Image�v���n�u��z�u
    public GameObject imagePrefab;   // �\������Image�v���n�u

    void Start()
    {
        DisplayCollectedImages();
    }

    void DisplayCollectedImages()
    {
        List<string> collectedImages = CollectibleManager.Instance.GetCollectedImages();

        foreach (string imageName in collectedImages)
        {
            GameObject imageObject = Instantiate(imagePrefab, imageContainer);
            imageObject.GetComponentInChildren<TextMeshProUGUI>().text = imageName;
            // imageObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(imageName); // �摜��ݒ肷��ꍇ
        }
    }
}
