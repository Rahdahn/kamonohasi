using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DropAreaManager : MonoBehaviour
{
    public static DropAreaManager Instance { get; private set; }

    public List<Draggable> dropObjs; // ドラッグ可能なオブジェクトのリスト
    public MonoBehaviour dropArea1;
    public MonoBehaviour dropArea2;
    public MonoBehaviour dropArea3;
    public MonoBehaviour dropArea4;
    public SliderMove1 sliderMove1;

    private Dictionary<MonoBehaviour, bool> dropAreaOccupied; // 各ドロップエリアが占有されているかどうかを管理
    private Dictionary<MonoBehaviour, string> dropAreaTags; // 各ドロップエリアに配置されたオブジェクトのタグを管理

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
        if (dropArea1 == null || dropArea2 == null || dropArea3 == null || dropArea4 == null)
        {
            UnityEngine.Debug.LogError("One or more drop areas are not assigned.");
            return;
        }

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
            if (dropObj == null)
            {
                UnityEngine.Debug.LogError("Drop object is null.");
                continue;
            }

            dropObj.snapPositions = new Dictionary<MonoBehaviour, Vector2>
            {
                { dropArea1, GetSnapPosition(dropArea1) },
                { dropArea2, GetSnapPosition(dropArea2) },
                { dropArea3, GetSnapPosition(dropArea3) },
                { dropArea4, GetSnapPosition(dropArea4) }
            };

            dropObj.onDropSuccess = (area, resetAction) =>
            {
                UnityEngine.Debug.Log("ドラッグ成功時");

                if (dropAreaOccupied[area])
                {
                    UnityEngine.Debug.Log("このエリアはすでに使用されています。");
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
                UnityEngine.Debug.Log("ドラッグ失敗時");
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
        Dictionary<string, float> dropOrderPercentages = new Dictionary<string, float>
        {
            { "D1,D5,D4,D3", 41.1f },
            { "D1,D5,D3,D4", 92.2f },
            { "D1,D3,D4,D5", 90.0f },
            { "D1,D3,D5,D4", 75.4f },
            { "D1,D4,D5,D3", 33.3f },
            { "D1,D4,D3,D5", 8.2f },
            { "D5,D1,D4,D3", 92.1f },
            { "D5,D1,D3,D4", 47.1f },
            { "D5,D4,D1,D3", 8.6f },
            { "D5,D4,D3,D1", 70.6f },
            { "D5,D3,D1,D4", 70.0f },
            { "D5,D3,D4,D1", 34.2f },
            { "D4,D5,D3,D1", 81.6f },
            { "D4,D5,D1,D3", 58.8f },
            { "D4,D1,D3,D5", 5.1f },
            { "D4,D1,D5,D3", 33.8f },
            { "D4,D3,D1,D5", 79.7f },
            { "D4,D3,D5,D1", 53.4f },
            { "D3,D1,D4,D5", 99.9f },
            { "D3,D1,D5,D4", 95.2f },
            { "D3,D4,D1,D5", 55.7f },
            { "D3,D4,D5,D1", 2.3f },
            { "D3,D5,D4,D1", 91.6f },
            { "D3,D5,D1,D4", 80.3f }
        };

        if (dropOrderPercentages.TryGetValue(dropOrder, out float percentage))
        {
            return percentage;
        }

        return 0.0f; // デフォルト値として0.0fを返す
    }

    void StartSliderMove1()
    {
        sliderMove1.gameObject.SetActive(true);
    }
}
