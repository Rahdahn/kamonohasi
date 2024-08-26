using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshProを使用
using UnityEngine.UI;

public class CollectSceneManager : MonoBehaviour
{
    public Transform imageContainer; // ここにImageプレハブを配置
    public GameObject imagePrefab;   // 表示するImageプレハブ

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
            // imageObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(imageName); // 画像を設定する場合
        }
    }
}
