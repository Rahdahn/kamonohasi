using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    private static CollectibleManager instance;

    public static CollectibleManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObject = new GameObject();
                instance = singletonObject.AddComponent<CollectibleManager>();
                DontDestroyOnLoad(singletonObject);
            }
            return instance;
        }
    }

    private List<string> collectedImages;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCollectedImages();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCollectedImage(string imageName)
    {
        if (!collectedImages.Contains(imageName))
        {
            collectedImages.Add(imageName);
            SaveCollectedImages();
        }
    }

    public List<string> GetCollectedImages()
    {
        return collectedImages;
    }

    private void LoadCollectedImages()
    {
        collectedImages = new List<string>();
        int count = PlayerPrefs.GetInt("CollectedImageCount", 0);
        for (int i = 0; i < count; i++)
        {
            string imageName = PlayerPrefs.GetString($"CollectedImage_{i}", "");
            if (!string.IsNullOrEmpty(imageName))
            {
                collectedImages.Add(imageName);
            }
        }
    }

    private void SaveCollectedImages()
    {
        PlayerPrefs.SetInt("CollectedImageCount", collectedImages.Count);
        for (int i = 0; i < collectedImages.Count; i++)
        {
            PlayerPrefs.SetString($"CollectedImage_{i}", collectedImages[i]);
        }
        PlayerPrefs.Save();
    }
}
