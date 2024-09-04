using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance { get; private set; }

    private Dictionary<string, int> collectedAnimals;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        collectedAnimals = new Dictionary<string, int>();
    }

    public void AddCollectedImage(string tag)
    {
        if (collectedAnimals.ContainsKey(tag))
        {
            collectedAnimals[tag]++;
        }
        else
        {
            collectedAnimals[tag] = 1;
        }

        UpdateUI(tag);
    }

    public void ReduceCollectedCount(string tag)
    {
        if (collectedAnimals.ContainsKey(tag) && collectedAnimals[tag] > 0)
        {
            collectedAnimals[tag]--;
            UpdateUI(tag);
        }
    }

    private void UpdateUI(string tag)
    {
        // Šl“¾‚µ‚½“®•¨‚Ìí—Ş‚Æ”‚ğUI‚É”½‰f‚·‚éˆ—‚ğ‚±‚±‚É’Ç‰Á
    }

    public int GetCollectedCount(string tag)
    {
        return collectedAnimals.ContainsKey(tag) ? collectedAnimals[tag] : 0;
    }
}
