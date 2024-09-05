using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class DropAreaManager : MonoBehaviour
{
    public static DropAreaManager Instance { get; private set; }

    public GameObject dropArea1;
    public GameObject dropArea2;
    public GameObject dropArea3;
    public GameObject dropArea4;
    public SliderMove1 sliderMove1;

    private Dictionary<GameObject, bool> dropAreaOccupied;
    private Dictionary<GameObject, string> dropAreaTags;

    public AnimalCounter animalCounter;  // AnimalCounter�̎Q�Ƃ�����

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
            Debug.LogError("One or more drop areas are not assigned.");
            return;
        }

        dropAreaOccupied = new Dictionary<GameObject, bool>
        {
            { dropArea1, false },
            { dropArea2, false },
            { dropArea3, false },
            { dropArea4, false }
        };

        dropAreaTags = new Dictionary<GameObject, string>
        {
            { dropArea1, null },
            { dropArea2, null },
            { dropArea3, null },
            { dropArea4, null }
        };

        InitializeDraggables();
    }

    private void InitializeDraggables()
    {
        var draggables = FindObjectsOfType<Draggable>();
        var dropAreaList = new List<RectTransform>
        {
            dropArea1.GetComponent<RectTransform>(),
            dropArea2.GetComponent<RectTransform>(),
            dropArea3.GetComponent<RectTransform>(),
            dropArea4.GetComponent<RectTransform>()
        };

        foreach (var draggable in draggables)
        {
            draggable.SetDropAreas(dropAreaList);

            draggable.snapPositions = new Dictionary<RectTransform, Vector2>
            {
                { dropArea1.GetComponent<RectTransform>(), GetSnapPosition(dropArea1) },
                { dropArea2.GetComponent<RectTransform>(), GetSnapPosition(dropArea2) },
                { dropArea3.GetComponent<RectTransform>(), GetSnapPosition(dropArea3) },
                { dropArea4.GetComponent<RectTransform>(), GetSnapPosition(dropArea4) }
            };

            draggable.onDropSuccess = (area, resetAction) =>
            {
                Debug.Log("�h���b�O������");

                if (dropAreaOccupied[area.gameObject])
                {
                    Debug.Log("���̃G���A�͂��łɎg�p����Ă��܂��B");
                    resetAction.Invoke();
                }
                else
                {
                    dropAreaOccupied[area.gameObject] = true;
                    draggable.transform.position = GetSnapPosition(area.gameObject);
                    dropAreaTags[area.gameObject] = draggable.tag;

                    // �����̃J�E���g�����炷
                    ReduceAnimalCount(draggable.tag);

                    CheckAllDropAreasFilled();
                }
            };

            draggable.onDropFail = (Action resetAction) =>
            {
                Debug.Log("�h���b�O���s��");
                resetAction.Invoke();
            };
        }
    }

    Vector2 GetSnapPosition(GameObject dropArea)
    {
        return dropArea.GetComponent<RectTransform>().position;
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
            // �f�[�^�Z�b�g�̗�i���ۂ̃f�[�^�ɍ��킹�ĕύX�j
            { "D1,D5,D4,D3", 41.1f },
            { "D1,D5,D3,D4", 92.2f },
            // ...
        };

        if (dropOrderPercentages.TryGetValue(dropOrder, out float percentage))
        {
            return percentage;
        }

        return 0.0f;
    }

    void StartSliderMove1()
    {
        sliderMove1.gameObject.SetActive(true);
    }

    // �����̃J�E���g�����炷����
    void ReduceAnimalCount(string tag)
    {
        switch (tag)
        {
            case "D1":
                if (GameData.D1Count > 0) GameData.D1Count--;
                break;
            case "D2":
                if (GameData.D2Count > 0) GameData.D2Count--;
                break;
            case "D3":
                if (GameData.D3Count > 0) GameData.D3Count--;
                break;
            case "D4":
                if (GameData.D4Count > 0) GameData.D4Count--;
                break;
            case "D5":
                if (GameData.D5Count > 0) GameData.D5Count--;
                break;
            default:
                Debug.LogWarning("Unknown animal tag: " + tag);
                break;
        }

        // �J�E���g�����炵�����UI���X�V
        animalCounter.UpdateAnimalCountDisplay();
    }
}
