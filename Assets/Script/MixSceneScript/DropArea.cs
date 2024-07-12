using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private List<string> dropOrder;

    void Start()
    {
        dropAreaOccupied = new Dictionary<MonoBehaviour, bool>
        {
            { dropArea1, false },
            { dropArea2, false },
            { dropArea3, false },
            { dropArea4, false }
        };

        dropOrder = new List<string>();

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
                    dropOrder.Add(dropObj.tag); // �I�u�W�F�N�g�̃^�O�����Ԃɒǉ�

                    // �I�u�W�F�N�g���X�i�b�v�ʒu�Ɉړ�
                    dropObj.transform.position = GetSnapPosition(area);

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
        // ���ׂẴG���A�����܂��Ă���ꍇ�A���Ԃ�ۑ�����sliderMove1���N��
        SaveDropOrderAndStartSliderMove();
    }

    void SaveDropOrderAndStartSliderMove()
    {
        string order = string.Join(",", dropOrder);
        PlayerPrefs.SetString("DropOrder", order);
        PlayerPrefs.SetFloat("DropOrderPercentage", GetPercentageByOrder(order));
        sliderMove1.gameObject.SetActive(true); // sliderMove1���A�N�e�B�u�ɂ���
    }

    float GetPercentageByOrder(string order)
    {
        // �\�̃p�[�Z���e�[�W��Ή��t���鎫����ݒ�
        var percentages = new Dictionary<string, float>
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

        if (percentages.ContainsKey(order))
        {
            return percentages[order];
        }
        return 0.0f; // �f�t�H���g�l
    }
}
