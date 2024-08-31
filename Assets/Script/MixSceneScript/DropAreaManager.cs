using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropAreaManager : MonoBehaviour
{
    public static DropAreaManager Instance { get; private set; }

    public List<Draggable> dropObjs;
    public MonoBehaviour dropArea1;
    public MonoBehaviour dropArea2;
    public MonoBehaviour dropArea3;
    public MonoBehaviour dropArea4;
    public SliderMove1 sliderMove1;

    private Dictionary<MonoBehaviour, bool> dropAreaOccupied;
    private Dictionary<MonoBehaviour, string> dropAreaTags;

    public List<MonoBehaviour> DropAreas
    {
        get
        {
            return new List<MonoBehaviour> { dropArea1, dropArea2, dropArea3, dropArea4 };
        }
    }

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
    }

    private void Start()
    {
        dropAreaOccupied = new Dictionary<MonoBehaviour, bool>
        {
            { dropArea1, false },
            { dropArea2, false },
            { dropArea3, false },
            { dropArea4, false }
        };

        dropAreaTags = new Dictionary<MonoBehaviour, string>
        {
            { dropArea1, null },
            { dropArea2, null },
            { dropArea3, null },
            { dropArea4, null }
        };

        foreach (var dropObj in dropObjs)
        {
            dropObj.snapPositions = new Dictionary<MonoBehaviour, Vector2>
            {
                { dropArea1, GetSnapPosition(dropArea1) },
                { dropArea2, GetSnapPosition(dropArea2) },
                { dropArea3, GetSnapPosition(dropArea3) },
                { dropArea4, GetSnapPosition(dropArea4) }
            };

            dropObj.onDropSuccess = (MonoBehaviour area, Action resetAction) =>
            {
                Debug.Log("ドラッグ成功時");

                if (dropAreaOccupied[area])
                {
                    Debug.Log("このエリアはすでに使用されています。");
                    resetAction.Invoke();
                }
                else
                {
                    dropAreaOccupied[area] = true;
                    dropObj.transform.position = GetSnapPosition(area);
                    dropAreaTags[area] = dropObj.tag;

                    // 獲得したImageを保存
                    CollectibleManager.Instance.AddCollectedImage(dropObj.tag);

                    CheckAllDropAreasFilled();
                }
            };

            dropObj.onDropFail = (Action resetAction) =>
            {
                Debug.Log("ドラッグ失敗時");
                resetAction.Invoke();
            };
        }
    }

    Vector2 GetSnapPosition(MonoBehaviour dropArea)
    {
        return dropArea.transform.position;
    }

    void CheckAllDropAreasFilled()
    {
        foreach (var occupied in dropAreaOccupied.Values)
        {
            if (!occupied)
            {
                return;
            }
        }
        SaveDropOrder();
        StartSliderMove1();
    }

    void SaveDropOrder()
    {
        List<string> dropOrder = new List<string>
        {
            dropAreaTags[dropArea1],
            dropAreaTags[dropArea2],
            dropAreaTags[dropArea3],
            dropAreaTags[dropArea4]
        };

        string dropOrderString = string.Join(",", dropOrder);
        PlayerPrefs.SetString("DropOrder", dropOrderString);
        PlayerPrefs.SetFloat("DropOrderPercentage", CalculatePercentage(dropOrderString));
    }

    float CalculatePercentage(string dropOrder)
    {
        switch (dropOrder)
        {
            case "D1,D5,D4,D3": return 41.1f;
            case "D1,D5,D3,D4": return 92.2f;
            case "D1,D3,D4,D5": return 90.0f;
            case "D1,D3,D5,D4": return 75.4f;
            case "D1,D4,D5,D3": return 33.3f;
            case "D1,D4,D3,D5": return 8.2f;
            case "D5,D1,D4,D3": return 92.1f;
            case "D5,D1,D3,D4": return 47.1f;
            case "D5,D4,D1,D3": return 8.6f;
            case "D5,D4,D3,D1": return 70.6f;
            case "D5,D3,D1,D4": return 70.0f;
            case "D5,D3,D4,D1": return 34.2f;
            case "D4,D5,D3,D1": return 81.6f;
            case "D4,D5,D1,D3": return 58.8f;
            case "D4,D1,D3,D5": return 5.1f;
            case "D4,D1,D5,D3": return 33.8f;
            case "D4,D3,D1,D5": return 79.7f;
            case "D4,D3,D5,D1": return 53.4f;
            case "D3,D1,D4,D5": return 99.9f;
            case "D3,D1,D5,D4": return 95.2f;
            case "D3,D4,D1,D5": return 55.7f;
            case "D3,D4,D5,D1": return 2.3f;
            case "D3,D5,D4,D1": return 91.6f;
            case "D3,D5,D1,D4": return 80.3f;
            default: return 0.0f;
        }
    }

    void StartSliderMove1()
    {
        sliderMove1.gameObject.SetActive(true);
    }
}
