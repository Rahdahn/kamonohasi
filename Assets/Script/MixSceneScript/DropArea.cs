using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // �X���C�_�[�p�̖��O��Ԃ�ǉ�
using UnityEngine.SceneManagement;

public class DropArea : MonoBehaviour
{
    // �h���b�v�\�I�u�W�F�N�g���X�g
    public List<Draggable> dropObjs;

    // �h���b�v�G���A�i�G�f�B�^�ŃA�^�b�`�j
    public MonoBehaviour dropArea1;
    public MonoBehaviour dropArea2;
    public MonoBehaviour dropArea3;
    public MonoBehaviour dropArea4;

    // sliderMove1�i�G�f�B�^�ŃA�^�b�`�j
    public SliderMove1 sliderMove1;

    // �e�h���b�v�G���A�̏�Ԃ�ǐՂ��鎫��
    private Dictionary<MonoBehaviour, bool> dropAreaOccupied;

    // �h���b�v�G���A���ƂɃh���b�v���ꂽ�I�u�W�F�N�g�̃^�O��ێ����鎫��
    private Dictionary<MonoBehaviour, string> dropAreaTags;

    void Start()
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
            // �h���b�v�G���A�ƃX�i�b�v�ʒu�̐ݒ�
            dropObj.snapPositions = new Dictionary<MonoBehaviour, Vector2>
            {
                { dropArea1, GetSnapPosition(dropArea1) },
                { dropArea2, GetSnapPosition(dropArea2) },
                { dropArea3, GetSnapPosition(dropArea3) },
                { dropArea4, GetSnapPosition(dropArea4) }
            };

            dropObj.onDropSuccess = (MonoBehaviour area, Action resetAction) =>
            {
                Debug.Log("�h���b�O������");

                if (dropAreaOccupied[area])
                {
                    Debug.Log("���̃G���A�͂��łɎg�p����Ă��܂��B");
                    resetAction.Invoke();
                }
                else
                {
                    dropAreaOccupied[area] = true;
                    // �I�u�W�F�N�g���X�i�b�v�ʒu�Ɉړ�
                    dropObj.transform.position = GetSnapPosition(area);

                    // �h���b�v�G���A�Ƀh���b�v���ꂽ�I�u�W�F�N�g�̃^�O���L�^
                    dropAreaTags[area] = dropObj.tag;

                    CollectibleManager.Instance.AddCollectedImage(dropObj.tag);

                    // ���ׂẴh���b�v�G���A�����܂������`�F�b�N
                    CheckAllDropAreasFilled();
                }
            };
            dropObj.onDropFail = (Action resetAction) =>
            {
                Debug.Log("�h���b�O���s��");
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
                return; // �܂����܂��Ă��Ȃ��G���A������ꍇ�͏I��
            }
        }

        // ���ׂẴG���A�����܂��Ă���ꍇ�A�h���b�v������ۑ�����sliderMove1���N��
        SaveDropOrder();
        StartsliderMove1();
    }

    void SaveDropOrder()
    {
        List<string> dropOrder = new List<string>
        {
<<<<<<< HEAD
            dropAreaTags[dropArea1],
            dropAreaTags[dropArea2],
            dropAreaTags[dropArea3],
            dropAreaTags[dropArea4]
=======
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
            { "D3,D1,D4,D5", 100.0f },
            { "D3,D1,D5,D4", 95.2f },
            { "D3,D4,D1,D5", 55.7f },
            { "D3,D4,D5,D1", 2.3f },
            { "D3,D5,D4,D1", 91.6f },
            { "D3,D5,D1,D4", 80.3f }
>>>>>>> 457fdfa6479a51bd306bec74465d155b2d58faa0
        };

        string dropOrderString = string.Join(",", dropOrder);
        PlayerPrefs.SetString("DropOrder", dropOrderString);
        PlayerPrefs.SetFloat("DropOrderPercentage", CalculatePercentage(dropOrderString));
    }

    float CalculatePercentage(string dropOrder)
    {
        // �����Ƀp�[�Z���e�[�W���v�Z���郍�W�b�N��ǉ�
        // ��: �C�ӂ̏����ɉ����ăp�[�Z���e�[�W��ݒ肷��
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

    void StartsliderMove1()
    {
        sliderMove1.gameObject.SetActive(true); // sliderMove1���A�N�e�B�u�ɂ���
    }
}
