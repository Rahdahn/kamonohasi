using System;
using System.Collections.Generic;
using UnityEngine;

public class DraggableManager : MonoBehaviour
{
    public GameObject d1Prefab;
    public GameObject d2Prefab;
    public GameObject d3Prefab;
    public GameObject d4Prefab;
    public GameObject d5Prefab;

    public List<MonoBehaviour> dropAreas;
    public SliderMove1 sliderMove1;

    private Dictionary<MonoBehaviour, bool> dropAreaOccupied;
    private Dictionary<MonoBehaviour, string> dropAreaTags;
    private Dictionary<Draggable, Dictionary<MonoBehaviour, Vector2>> draggableSnapPositions;

    private void Start()
    {
        InitializeDropAreas();
        InitializeDraggableAnimals();
    }

    private void InitializeDropAreas()
    {
        dropAreaOccupied = new Dictionary<MonoBehaviour, bool>();
        dropAreaTags = new Dictionary<MonoBehaviour, string>();

        foreach (var dropArea in dropAreas)
        {
            dropAreaOccupied[dropArea] = false;
            dropAreaTags[dropArea] = null;
        }
    }

    private void InitializeDraggableAnimals()
    {
        draggableSnapPositions = new Dictionary<Draggable, Dictionary<MonoBehaviour, Vector2>>();

        CreateDraggableAnimals(AnimalType.D1, d1Prefab, GameData.D1Count);
        CreateDraggableAnimals(AnimalType.D2, d2Prefab, GameData.D2Count);
        CreateDraggableAnimals(AnimalType.D3, d3Prefab, GameData.D3Count);
        CreateDraggableAnimals(AnimalType.D4, d4Prefab, GameData.D4Count);
        CreateDraggableAnimals(AnimalType.D5, d5Prefab, GameData.D5Count);
    }

    private void CreateDraggableAnimals(AnimalType type, GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject animal = Instantiate(prefab);
            animal.transform.position = prefab.transform.position;
            animal.transform.localScale = prefab.transform.localScale;

            Draggable draggable = animal.GetComponent<Draggable>();
            if (draggable != null)
            {
                draggable.snapPositions = new Dictionary<MonoBehaviour, Vector2>();
                foreach (var dropArea in dropAreas)
                {
                    draggable.snapPositions[dropArea] = GetSnapPosition(dropArea);
                }

                draggable.beforeBeginDrag = () =>
                {
                    Debug.Log($"{type} Dragging started.");
                };
                draggable.onDropSuccess = (area, reset) =>
                {
                    Debug.Log($"{type} Dropped in area.");
                    if (dropAreaOccupied[area])
                    {
                        Debug.Log("このエリアはすでに使用されています。");
                        reset?.Invoke();
                    }
                    else
                    {
                        dropAreaOccupied[area] = true;
                        animal.transform.position = GetSnapPosition(area);
                        dropAreaTags[area] = animal.tag;

                        CollectibleManager.Instance.AddCollectedImage(animal.tag);
                        CheckAllDropAreasFilled();
                    }
                };
                draggable.onDropFail = reset =>
                {
                    Debug.Log($"{type} Drop failed.");
                    reset?.Invoke();
                };

                draggableSnapPositions[draggable] = draggable.snapPositions;
            }
        }
    }

    private Vector2 GetSnapPosition(MonoBehaviour dropArea)
    {
        return dropArea.transform.position;
    }

    private void CheckAllDropAreasFilled()
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

    private void SaveDropOrder()
    {
        List<string> dropOrder = new List<string>
        {
            dropAreaTags[dropAreas[0]],
            dropAreaTags[dropAreas[1]],
            dropAreaTags[dropAreas[2]],
            dropAreaTags[dropAreas[3]]
        };

        string dropOrderString = string.Join(",", dropOrder);
        PlayerPrefs.SetString("DropOrder", dropOrderString);
        PlayerPrefs.SetFloat("DropOrderPercentage", CalculatePercentage(dropOrderString));
    }

    private float CalculatePercentage(string dropOrder)
    {
        // Percentage calculation logic as before
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

    private void StartSliderMove1()
    {
        sliderMove1.gameObject.SetActive(true);
    }
}
